package com.example.thoughtclan.conversion.service.impl;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.*;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.thoughtclan.conversion.entity.*;
import com.example.thoughtclan.conversion.repository.*;
import com.example.thoughtclan.conversion.service.ExtractionService;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.node.ArrayNode;
import com.fasterxml.jackson.dataformat.xml.XmlMapper;

@Service
public class ExtractionServiceImpl implements ExtractionService {
	
	@Autowired
	private ToolsRepository toolsRepository;
	
	@Autowired
	private SASTOverviewRepository sastOverviewRepository;
	
	@Autowired
	private SASTScanRepository sastScanRepository;
	
	@Autowired
	private SASTVulnerabilitiesRepository sastVulnerabilitiesRepository;
	
	@Autowired
	private ApplicationRepository applicationRepository;

	@Override
	public String parcingTheXmlFile(byte[] inputstream) {
		try {
			List<Tools> tools = toolsRepository.findAll();
			Tools tool = new Tools();
			if(tools.isEmpty()) {
				tool.setName("veracode");
				tool = toolsRepository.save(tool);
			} else {
				tool = tools.get(0);
			}
			
			Optional<Application> optionalApplication = applicationRepository.findByApplicationKey("application one");
			Application application = new Application();
			if(!optionalApplication.isPresent()) {
				application.setName("application one");
				application = applicationRepository.save(application);
			} else {
				application = optionalApplication.get();
			}
			
			DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss z");
			SASTScan sastScan = new SASTScan();
			XmlMapper xmlMapper = new XmlMapper();
			JsonNode rootNode = xmlMapper.readTree(inputstream);
			
			// Checking the existing sast scan based on scan id and build id
			sastScan.setScanId(rootNode.has("analysis_id") ? rootNode.get("analysis_id").asText() : null);
			sastScan.setBuildId(rootNode.has("build_id") ? rootNode.get("build_id").asText() : null);
			sastScan.setScannedOn(rootNode.has("generation_date") ? LocalDateTime.parse(rootNode.get("generation_date").asText(), formatter) : null);
			List<SASTScan> existingSastScans = sastScanRepository.findByScanIdAndBuildId(sastScan.getScanId(), sastScan.getBuildId());
			LocalDate localDate = sastScan.getScannedOn().toLocalDate();
			if(existingSastScans.stream().filter(item -> item.getScannedOn().toLocalDate().equals(localDate)).count() > 0) {
				return "Already parsed this xml file";
			}
			sastScan.setName(rootNode.has("app_name") ? rootNode.get("app_name").asText() : null);
			sastScan.setScannedBy(rootNode.has("submitter") ? rootNode.get("submitter").asText() : null);
			sastScan.setTotalFlaws(rootNode.has("total_flaws") ? Long.valueOf(rootNode.get("total_flaws").asText()) : null);
			sastScan.setFlawsNotMitigated(rootNode.has("flaws_not_mitigated") ? Long.valueOf(rootNode.get("flaws_not_mitigated").asText()) : null);
			sastScan.setStaticAnalysisUnitId(rootNode.has("static_analysis_unit_id") ? rootNode.get("static_analysis_unit_id").asText() : null);
			sastScan.setPolicyName(rootNode.has("policy_name") ? rootNode.get("policy_name").asText() : null);
			sastScan.setPolicyComplianceStatus(rootNode.has("policy_compliance_status") ? rootNode.get("policy_compliance_status").asText() : null);
			sastScan.setAnalysisRating(rootNode.has("static-analysis") && rootNode.get("static-analysis").has("rating") ? rootNode.get("static-analysis").get("rating").asText() : null );
			sastScan.setToolAppId(rootNode.has("app_id") ? rootNode.get("app_id").asText() : null);
			sastScan.setToolAccountId(rootNode.has("account_id") ? rootNode.get("account_id").asText() : null);
			sastScan.setScore(rootNode.has("static-analysis") && rootNode.get("static-analysis").has("score") ? rootNode.get("static-analysis").get("score").asText() : null);
			sastScan.setToolId(tool); // setting tool
			sastScan.setApplication(application);
//			Need to set application and bit bucket
			sastScan = sastScanRepository.save(sastScan);
			
			List<SASTVulnerabilities> listOfSASTVulnerabilities = new ArrayList<>();
			if(rootNode.get("severity").isArray()) {
				ArrayNode arrayNode = (ArrayNode) rootNode.get("severity");
				Iterator<JsonNode> severityList = arrayNode.elements();
				while(severityList.hasNext()) {
			    	JsonNode severity = severityList.next();
			    	List<JsonNode> categoryList = new ArrayList<>();
					if(severity.has("category") && severity.get("category").isArray()){
						ArrayNode array1Node = (ArrayNode) severity.get("category");
						Iterator<JsonNode> category = array1Node.elements();
						category.forEachRemaining(categoryList::add);
			    	} else {
			    		if(severity.has("category")){
			    			categoryList.add(severity.get("category"));
			    		}
			    	}
					for(JsonNode category : categoryList) {
						String recommendations = category.path("recommendations").path("para").path("text").asText();
						if(category.has("cwe")) {
							List<JsonNode> cweList = new ArrayList<>();
							if(category.get("cwe").isArray()) {
								ArrayNode array2Node = (ArrayNode) category.get("cwe");
								Iterator<JsonNode> cwe = array2Node.elements();
								cwe.forEachRemaining(cweList::add);
							} else {
								JsonNode cwe = category.get("cwe");
								cweList.add(cwe);
							}
							for(JsonNode cwe : cweList) {
								String cwename = cwe.has("cwename") ? cwe.get("cwename").asText() : null;
								String cweDescription = cwe.has("description") && cwe.get("description").has("text") && cwe.get("description").get("text").has("text") ? cwe.path("description").path("text").path("text").asText() : null;
								JsonNode flw = cwe.get("staticflaws");
								if(flw.has("flaw")) {
									List<JsonNode> flawsList = new ArrayList<>();
									if(flw.get("flaw").isArray()) {
										ArrayNode array3Node = (ArrayNode) flw.get("flaw");
										Iterator<JsonNode> flaws = array3Node.elements();
										flaws.forEachRemaining(flawsList::add);
									} else {
										JsonNode flaw = flw.get("flaw");
										flawsList.add(flaw);
									}
									for(JsonNode flaw : flawsList) {
										SASTVulnerabilities sastVulnerabilities = new SASTVulnerabilities();
										sastVulnerabilities.setCategoryId(flaw.has("categoryid") ? Long.valueOf(flaw.get("categoryid").asText()) : null);
										sastVulnerabilities.setCategoryName(flaw.has("categoryname") ? flaw.get("categoryname").asText() : null);
										sastVulnerabilities.setDescription(flaw.has("description") ? flaw.get("description").asText() : null);
										sastVulnerabilities.setSeverityLevel(flaw.has("severity") ? flaw.get("severity").asText() : null);
										sastVulnerabilities.setRemediationStatus(flaw.has("remediation_status") ? flaw.get("remediation_status").asText() : null);
										sastVulnerabilities.setRecommendations(recommendations);
										sastVulnerabilities.setDateFirstOccurrence(flaw.has("date_first_occurrence") ? LocalDateTime.parse(flaw.get("date_first_occurrence").asText(), formatter) : null);
										sastVulnerabilities.setCweId(flaw.has("cweid") ? Long.valueOf(flaw.get("cweid").asText()) : null);
										sastVulnerabilities.setCweName(cwename);
										sastVulnerabilities.setCweDescription(cweDescription);
										sastVulnerabilities.setCount(flaw.has("count") ? Long.valueOf(flaw.get("count").asText()) : null);
										sastVulnerabilities.setModule(flaw.has("module") ? flaw.get("module").asText() : null);
										sastVulnerabilities.setIssueId(flaw.has("issueid") ? flaw.get("issueid").asText() : null);
										sastVulnerabilities.setSourceFile(flaw.has("sourcefile") ? flaw.get("sourcefile").asText() : null);
										sastVulnerabilities.setSourceFilePath(flaw.has("sourcefilepath") ? flaw.get("sourcefilepath").asText() : null);
										sastVulnerabilities.setLineNumber(flaw.has("line") ? Long.valueOf(flaw.get("line").asText()) : null);
										sastVulnerabilities.setFunctionPrototype(flaw.has("functionprototype") ? flaw.get("functionprototype").asText() : null);
										sastVulnerabilities.setSastScan(sastScan);
										listOfSASTVulnerabilities.add(sastVulnerabilities);
									}
								}
							}
						}
					}
					
			    }
			}
			sastVulnerabilitiesRepository.saveAll(listOfSASTVulnerabilities);
			SASTOverview sastOverview = new SASTOverview();
			Optional<SASTOverview> optionalSastOverview = sastOverviewRepository.findById(sastScan.getId());
			if(optionalSastOverview.isPresent() ) {
				sastOverview = optionalSastOverview.get();
			}
			if(rootNode.has("flaw-status")) {
				JsonNode flawStatus = rootNode.get("flaw-status");
				sastOverview.setNewFlaws(flawStatus.has("new") ? Long.valueOf(flawStatus.get("new").asText()) : null);
				sastOverview.setFlawsReopened(flawStatus.has("reopen") ? Long.valueOf(flawStatus.get("reopen").asText()) : null);
				sastOverview.setFlawsOpen(flawStatus.has("open") ? Long.valueOf(flawStatus.get("open").asText()) : null);
				sastOverview.setFlawsFixed(flawStatus.has("fixed") ? Long.valueOf(flawStatus.get("fixed").asText()) : null);
				sastOverview.setTotalFlaws(flawStatus.has("total") ? Long.valueOf(flawStatus.get("total").asText()) : null);
				sastOverview.setFlawsNotMitigated(flawStatus.has("not_mitigated") ? Long.valueOf(flawStatus.get("not_mitigated").asText()) : null);
			}
			
			if(rootNode.has("static-analysis") && rootNode.get("static-analysis").has("modules") && rootNode.get("static-analysis").get("modules").has("module")) {
				List<JsonNode> moduleList = new ArrayList<>();
				if(rootNode.get("static-analysis").get("modules").get("module").isArray()){
					ArrayNode array1Node = (ArrayNode) rootNode.get("static-analysis").get("modules").get("module");
					Iterator<JsonNode> module = array1Node.elements();
					module.forEachRemaining(moduleList::add);
		    	} else {
		    		moduleList.add(rootNode.get("static-analysis").get("modules").get("module"));
		    	}
				Long numflawssev0 = 0L;
				Long numflawssev1 = 0L;
				Long numflawssev2 = 0L;
				Long numflawssev3 = 0L;
				Long numflawssev4 = 0L;
				Long numflawssev5 = 0L;
				for(JsonNode module : moduleList) {
					numflawssev0 += (module.has("numflawssev0") ? module.get("numflawssev0").asLong() : 0L);
					numflawssev1 += (module.has("numflawssev1") ? module.get("numflawssev1").asLong() : 0L);
					numflawssev2 += (module.has("numflawssev2") ? module.get("numflawssev2").asLong() : 0L);
					numflawssev3 += (module.has("numflawssev3") ? module.get("numflawssev3").asLong() : 0L);
					numflawssev4 += (module.has("numflawssev4") ? module.get("numflawssev4").asLong() : 0L);
					numflawssev5 += (module.has("numflawssev5") ? module.get("numflawssev5").asLong() : 0L);
				}
				sastOverview.setTotalFlawsSev0(numflawssev0);
				sastOverview.setTotalFlawsSev1(numflawssev1);
				sastOverview.setTotalFlawsSev2(numflawssev2);
				sastOverview.setTotalFlawsSev3(numflawssev3);
				sastOverview.setTotalFlawsSev4(numflawssev4);
				sastOverview.setTotalFlawsSev5(numflawssev5);
				sastOverview.setSastScan(sastScan);
				sastOverviewRepository.save(sastOverview);
			}
			return "Success";
		} catch (Exception e) {
			e.printStackTrace();
        }
		return "Failed";
	}

}
