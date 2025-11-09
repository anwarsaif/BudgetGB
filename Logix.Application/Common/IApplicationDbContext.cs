using Logix.Domain.ACC;
using Logix.Domain.CRM;
using Logix.Domain.FXA;
using Logix.Domain.Gb;
using Logix.Domain.GB;
using Logix.Domain.Hr;
using Logix.Domain.HR;
using Logix.Domain.Main;
using Logix.Domain.OPM;
using Logix.Domain.PM;
using Logix.Domain.PUR;
using Logix.Domain.RPT;
using Logix.Domain.SAL;
using Logix.Domain.WA;
using Logix.Domain.WF;
using Logix.Domain.WH;
using Microsoft.EntityFrameworkCore;


namespace Logix.Application.Common
{
    public interface IApplicationDbContext
    {

        #region ============== Main ======================
        DbSet<SysSystem> SysSystems { get; }
        DbSet<SysScreen> SysScreens { get; }
        DbSet<SysAnnouncement> SysAnnouncements { get; }
        DbSet<SysAnnouncementVw> SysAnnouncementVws { get; }
        DbSet<SysAnnouncementLocationVw> SysAnnouncementLocationVws { get; }
        DbSet<SysLookupCategory> SysLookupCategories { get; }
        DbSet<SysLookupData> SysLookupData { get; }
        DbSet<SysLookupDataVw> SysLookupDataVws { get; }
        DbSet<SysDepartment> SysDepartments { get; }
        DbSet<SysDepartmentVw> SysDepartmentVws { get; }
        DbSet<SysDepartmentCatagory> SysDepartmentCatagories { get; }
        DbSet<SysGroup> SysGroups { get; }
        DbSet<SysGroupVw> SysGroupVws { get; }
        DbSet<SysScreenPermission> SysScreenPermissions { get; }
        DbSet<SysScreenPermissionVw> SysScreenPermissionVws { get; }
        DbSet<SysBranchVw> SysBranchVws { get; }
        DbSet<SysNotification> SysNotifications { get; }
        DbSet<SysNotificationsVw> SysNotificationsVws { get; }
        DbSet<SysProperty> SysProperties { get; }
        DbSet<SysPropertiesVw> SysPropertiesVws { get; }
        DbSet<SysPropertyValue> SysPropertyValues { get; }
        DbSet<SysPropertyValuesVw> SysPropertyValuesVws { get; }
        DbSet<SysScreenProperty> SysScreenProperties { get; }
        DbSet<SysScreenPermissionProperty> SysScreenPermissionProperties { get; }
        DbSet<SysScreenPermissionPropertiesVw> SysScreenPermissionPropertiesVws { get; }
        DbSet<SysCustomerType> SysCustomerTypes { get; }
        DbSet<SysCustomerGroup> SysCustomerGroups { get;}
        DbSet<SysCustomerGroupAccount> SysCustomerGroupAccounts { get; }
        DbSet<SysCustomerGroupAccountsVw> SysCustomerGroupAccountsVws { get; }
        DbSet<SysLicense> SysLicenses { get; }
        DbSet<SysLicensesVw> SysLicensesVws { get; }
        DbSet<SysFavMenu> SysFavMenus { get; }
        DbSet<SysFile> SysFiles { get; }

        DbSet<InvestBranch> InvestBranches { get; }
        DbSet<InvestBranchVw> InvestBranceshVws { get; }

        DbSet<SysCurrency> SysCurrency { get; }
        DbSet<SysCurrencyListVw> SysCurrencyListVws { get; }

        DbSet<SysExchangeRate> SysExchangeRates { get; }
        DbSet<SysExchangeRateVw> SysExchangeRatesVws { get; }
        DbSet<SysExchangeRateListVW> SysExchangeRateListsVws { get; }

        DbSet<SysUser> SysUsers { get; }
        DbSet<SysUserVw> SysUserVws { get; }
        DbSet<SysScreenInstalled> SysScreenInstalleds { get; }
        DbSet<SysScreenInstalledVw> SysScreenInstalledVws { get; }
        DbSet<SysTasksVw> SysTasksVws { get; }
        DbSet<SysCites> SysCites { get; }

        DbSet<SysCustomerBranch> SysCustomerBranches { get; }
        DbSet<SysCustomerBranchVw> SysCustomerBranchVws { get; }
        DbSet<SysCustomerCoType> SysCustomerCoTypes { get; }

        DbSet<SysPoliciesProcedure> SysPoliciesProcedures { get; }
        DbSet<SysPoliciesProceduresVw> SysPoliciesProceduresVws { get; }

        DbSet<SysVatGroup> SysVatGroups { get; }
        DbSet<SysCustomerVw> SysCustomerVws { get; }
        DbSet<SysCustomerContact> SysCustomerContacts { get; }
        DbSet<SysCustomerContactVw> SysCustomerContactVws { get; }
        DbSet<SysCustomerFile> SysCustomerFiles { get; }
        DbSet<SysCustomerFilesVw> SysCustomerFilesVws { get; }

        DbSet<SysTemplate> SysTemplates { get; }
        DbSet<SysTemplateVw> SysTemplateVws { get; }

        DbSet<SysForm> SysForms { get; }
        DbSet<SysFormsVw> SysFormsVws { get; }

        DbSet<SysSettingExport> SysSettingExports { get; }
        DbSet<SysSettingExportVw> SysSettingExportVws { get; }
        DbSet<SysActivityLog> SysActivityLogs { get; }
        DbSet<SysActivityLogVw> SysActivityLogVws { get; }
        DbSet<SysUserLogTime> SysUserLogTimes { get; }
        DbSet<SysUserLogTimeVw> SysUserLogTimeVws { get; }
        DbSet<SysUserTracking> SysUserTrackings { get; }
        DbSet<SysUserTrackingVw> SysUserTrackingVws { get; }
       
        DbSet<Domain.Main.InvestEmployee> InvestEmployees { get; }
        DbSet<InvestEmployeeVvw> InvestEmployeeVvws { get; }

        DbSet<SysUserType> SysUserTypes { get; }
        DbSet<SysUserType2> SysUserTypes2 { get; }

        DbSet<SysVersion> SysVersions { get; }

        DbSet<SysDynamicAttribute> SysDynamicAttributes { get; }
        DbSet<SysDynamicAttributesVw> SysDynamicAttributesVws { get; }
        DbSet<SysDynamicAttributeDataType> SysDynamicAttributeDataTypes { get; }
        DbSet<SysScreenVw> SysScreenVws { get; }
        DbSet<SysVatGroupVw> SysVatGroupVws { get; }
        DbSet<SysMailServer> SysMailServer { get; }
        DbSet<SysCountry> SysCountrys { get; }
        DbSet<SysCountryVw> SysCountryVws { get; }

        DbSet<SysNotificationsMang> SysNotificationsMangs { get; } 
        DbSet<SysNotificationsMangVw> SysNotificationsMangVws { get; }

        DbSet<SysTable> SysTables { get; }
        DbSet<SysTableField> SysTableFields { get; }

        DbSet<SysActivityType> SysActivityTypes { get; }
        DbSet<SysPackagesPropertyValue> SysPackagesPropertyValue { get; }

        DbSet<SysZatcaInvoiceType> SysZatcaInvoiceTypes { get; }
        
        #endregion

        #region ======= HR ========================
        DbSet<HrEmployee> HrEmployees { get; }
        DbSet<HrEmployeeVw> HrEmployeeVws { get; }
        DbSet<HrAttDay> HrAttDays { get; }
        DbSet<HrEvaluationAnnualIncreaseConfig> HrEvaluationAnnualIncreaseConfigs { get; }  
        DbSet<HrNotification> HrNotifications { get; }
        DbSet<HrNotificationsVw> HrNotificationsVws { get; } 
        DbSet<HrCompetence> HrCompetences { get; }
        DbSet<HrCompetencesVw> HrCompetencesVws { get; } 
        DbSet<HrCompetencesCatagory> HrCompetencesCatagorys { get; }
        DbSet<HrKpiTemplatesCompetence> HrKpiTemplatesCompetences { get; }
    
        DbSet<HrTrainingBag> HrTrainingBags { get; } 
        DbSet<HrTrainingBagVw> HrTrainingBagVws { get; }
        DbSet<HrPolicy> HrPolicys { get; } 
        DbSet<HrPoliciesVw> HrPoliciesVws { get; } 
        DbSet<HrPoliciesType> HrPoliciesTypes { get; }
         DbSet<HrKpiTemplate> HrKpiTemplates { get; } 
        DbSet<HrKpiTemplatesVw> HrKpiTemplatesVws { get; }
        DbSet<HrSetting> HrSettings { get; }
        DbSet<HrSalaryGroup> HrSalaryGroups { get; }
        DbSet<HrSalaryGroupVw> HrSalaryGroupVws { get; } 
        DbSet<HrVacationsType> HrVacationsTypes { get; }
        DbSet<HrVacationsTypeVw> HrVacationsTypeVws { get; } 
        DbSet<HrDisciplinaryCase> HrDisciplinaryCases { get; }
        DbSet<HrAbsence> HrAbsences { get; }
        DbSet<HrVacations> HrVacations { get; }
        DbSet<HrDelay> HrDelays { get; }
        //DbSet<HrDisciplinaryCase> HrDisciplinaryCases { get; } 
        DbSet<HrCardTemplate> HrCardTemplates { get; }
        DbSet<InvestMonth> investMonths { get; }
        DbSet<HrOverTimeD> hrOverTimeDs { get; }
        DbSet<HrOverTimeDVw> hrOverTimeDVws { get; }
        DbSet<HrPayrollType> HrPayrollTypes { get; }

        DbSet<HrAttShift> HrAttShifts { get; }
        DbSet<HrJobProgramVw> HrJobProgramVws { get; }
        DbSet<HrJobVw> HrJobVws { get; }

        DbSet<HrDirectJob> HrDirectJobs { get;  }
        DbSet<HrDirectJobVw> HrDirectJobVws { get; }

        DbSet<HrNote> HrNotes { get; }
        DbSet<HrNoteVw> HrNoteVws { get; }

        DbSet<HrAllowanceDeductionM> HrAllowanceDeductionMs { get; }
        DbSet<HrAllowanceDeductionTempOrFix> HrAllowanceDeductionTempOrFixs { get; }
        DbSet<HrArchiveFilesDetail> HrArchiveFilesDetails { get; }
        DbSet<HrArchiveFilesDetailsVw> HrArchiveFilesDetailsVws { get; } 
        DbSet<HrAssignman> HrAssignmans { get; }
        DbSet<HrAssignmenVw> HrAssignmenVws { get; }
        DbSet<HrAttAction> HrAttActions { get; }
        DbSet<HrAttLocationEmployee> HrAttLocationEmployees { get; }
        DbSet<HrAttLocationEmployeeVw> HrAttLocationEmployeeVws { get; }
        DbSet<HrAttShiftClose> HrAttShiftCloses { get; }
        DbSet<HrAttShiftCloseVw> HrAttShiftCloseVws { get; }
        DbSet<HrAttShiftCloseD> HrAttShiftCloseDs { get; }
         DbSet<HrAuthorization> HrAuthorizations { get; }
        DbSet<HrAuthorizationVw> HrAuthorizationVws { get; }
        DbSet<HrAttendanceBioTime> HrAttendanceBioTimes { get; }
           DbSet<HrCheckInOut> HrCheckInOuts { get; }
        DbSet<HrCheckInOutVw> HrCheckInOutVws { get; }
        DbSet<HrClearance> HrClearances { get; }
        DbSet<HrClearanceVw> HrClearanceVws { get; } 
        DbSet<HrClearanceType> HrClearanceTypes { get; }
        DbSet<HrClearanceTypeVw> HrClearanceTypeVws { get; }
        DbSet<HrCompensatoryVacation> HrCompensatoryVacations { get; }
        DbSet<HrCompensatoryVacationsVw> HrCompensatoryVacationsVws { get; }
        DbSet<HrContracte> HrContractes { get; }
        DbSet<HrContractesVw> HrContractesVws { get; }
        DbSet<HrClearanceMonth> HrClearanceMonths { get; }
        DbSet<HrCostType> HrCostTypes { get; }
        DbSet<HrCostTypeVw> HrCostTypeVws { get; }
        DbSet<HrCustody> HrCustodys { get; }
        DbSet<HrCustodyVw> HrCustodyVws { get; }
        DbSet<HrCustodyItem> HrCustodyItems { get; }
        DbSet<HrCustodyItemsVw> HrCustodyItemsVws { get; }
        DbSet<HrCustodyItemsProperty> HrCustodyItemsPropertys { get; }
        DbSet<HrCustodyRefranceType> HrCustodyRefranceTypes { get; }
        DbSet<HrCustodyType> HrCustodyTypes { get; }
        DbSet<HrDecision> HrDecisions { get; }
        DbSet<HrDecisionsEmployee> HrDecisionsEmployees { get; }
        DbSet<HrDecisionsEmployeeVw> HrDecisionsEmployeeVws { get; }
        DbSet<HrHoliday> HrHolidays { get; }
        DbSet<HrHolidayVw> HrHolidayVws { get; }
        DbSet<HrPermission> HrPermissions { get; }
        DbSet<HrPermissionsVw> HrPermissionsVws { get; }
        DbSet<HrAttShiftTimeTable> HrAttShiftTimeTables { get; }
        DbSet<HrAttShiftTimeTableVw> HrAttShiftTimeTableVws { get; }
        DbSet<HrEmployeeCost> HrEmployeeCosts { get; }
        DbSet<HrInsurancePolicy> HrInsurancePolicys { get; }
        DbSet<HrInsurance> HrInsurances { get; }
        DbSet<HrInsuranceEmp> HrInsuranceEmps { get; }
        DbSet<HrInsuranceEmpVw> HrInsuranceEmpVws { get; }

        DbSet<HrEmployeeCostVw> HrEmployeeCostVws { get; }
         DbSet<HrJob> HrJobs { get;  }
         DbSet<HrJobDescription> HrJobDescriptions { get; }
         DbSet<HrJobEmployeeVw> HrJobEmployeeVws { get; }
         DbSet<HrJobLevel> HrJobLevels { get; }
         DbSet<HrRecruitmentVacancy> HrRecruitmentVacancys { get;  }
         DbSet<HrRecruitmentVacancyVw> HrRecruitmentVacancyVws { get; }
         DbSet<HrRecruitmentApplication> HrRecruitmentApplications { get; }
         DbSet<HrRecruitmentApplicationVw> HrRecruitmentApplicationVws { get;}
         DbSet<HrRecruitmentCandidate> HrRecruitmentCandidates { get;}
         DbSet<HrRecruitmentCandidateVw> HrRecruitmentCandidateVws { get;}
        DbSet<HrVacancyStatusVw> HrVacancyStatusVws { get; }
        DbSet<HrJobGrade> HrJobGrades { get; }
        DbSet<HrJobGradeVw> HrJobGradeVws { get; }

        DbSet<HrRecruitmentCandidateKpi> HrRecruitmentCandidateKpis { get; }
        DbSet<HrRecruitmentCandidateKpiVw> HrRecruitmentCandidateKpiVws { get; }
        DbSet<HrRecruitmentCandidateKpiD> HrRecruitmentCandidateKpiDs { get; }
        DbSet<HrRecruitmentCandidateKpiDVw> HrRecruitmentCandidateKpiDVws { get; }
        DbSet<HrPayroll> HrPayrolls { get; }
        DbSet<HrPayrollVw> HrPayrollVws { get; }
        DbSet<HrTicket> HrTickets { get; }
        DbSet<HrTicketVw> HrTicketVws { get; }
        DbSet<HrVisaVw> HrVisaVws { get; }
        DbSet<HrVisa> HrVisas { get; }
        DbSet<HrFixingEmployeeSalary> HrFixingEmployeeSalarys { get; }
        DbSet<HrFixingEmployeeSalaryVw> HrFixingEmployeeSalaryVws { get; }
        DbSet<HrLeaveType> HrLeaveTypes { get; }
        DbSet<HrLeaveTypeVw> HrLeaveTypeVws { get; }
        DbSet<HrPayrollAllowanceDeduction> HrPayrollAllowanceDeductions { get; }
        DbSet<HrPayrollAllowanceDeductionVw> HrPayrollAllowanceDeductionVws { get; }
        DbSet<HrLoanInstallmentPayment> HrLoanInstallmentPayments { get; }
        DbSet<HrLoanInstallmentPaymentVw> HrLoanInstallmentPaymentVws { get; }
        DbSet<HrLoanInstallment> HrLoanInstallments { get; }
        DbSet<HrLoanInstallmentVw> HrLoanInstallmentVws { get; }
        DbSet<HrPayrollNote> HrPayrollNotes { get; }
        DbSet<HrPayrollNoteVw> HrPayrollNoteVws { get; }
        DbSet<HrDecisionsType> HrDecisionsTypes { get; }
        DbSet<HrDecisionsTypeVw> HrDecisionsTypeVws { get; }
        DbSet<HrDecisionsTypeEmployee> HrDecisionsTypeEmployees { get; }
        DbSet<HrDecisionsTypeEmployeeVw> HrDecisionsTypeEmployeeVws { get; }

        DbSet<HrRequestVw> HrRequestVws { get; }


        #endregion

        #region ============ ACC ==============================================================================

        DbSet<AccAccount> AccAccounts { get; }
        DbSet<AccFinancialYear> AccFinancialYears { get; }
        DbSet<AccFacility> AccFacilities { get; }
        DbSet<AccFacilitiesVw> AccFacilitiesVws { get; }
        DbSet<AccGroup> AccGroup { get; }
        DbSet<AccPeriods> AccPeriods { get; }
        DbSet<AccCostCenterVws> AccCostCenterVws { get; }
        DbSet<AccJournalMaster> AccJournalMasters { get; }
        DbSet<AccJournalMasterVw> AccJournalMasterVws { get; }
        DbSet<AccCostCenter> AccCostCenter { get; }

        DbSet<AccReferenceType> AccReferenceTypes { get; }
        DbSet<AccAccountsVw> AccAccountsVw { get; }
        DbSet<WhItemsVw> WhItemsVws { get; }

        DbSet<AccBranchAccount> AccBranchAccounts { get; }
        DbSet<AccBranchAccountsVw> AccBranchAccountsVws { get; }
        DbSet<AccBranchAccountType> AccBranchAccountTypes { get; }

        DbSet<AccPeriodDateVws> AccPeriodDateVws { get; }
        DbSet<AccJournalDetaile> AccJournalDetailes { get; }
        DbSet<AccJournalDetailesVw> AccJournalDetailesVws { get; }
        DbSet<AccAccountsCloseType> AccAccountsCloseTypes { get; }
        DbSet<AccBank> AccBanks { get; }
        DbSet<AccCashOnHand> AccCashOnHands { get; }
        DbSet<AccRequest> AccRequests { get; }
        DbSet<AccRequestVw> AccRequestVws { get; }
        DbSet<AccDocumentTypeListVw> AccDocumentTypeListVws { get; }
        DbSet<AccCashOnHandListVw> AccCashonhandListVWs { get; }
        DbSet<AccAccountsSubHelpeVw> AccAccountsSubHelpeVws { get; }
        DbSet<AccCostCenteHelpVw> AccCostCenteHelpVws { get; }
        DbSet<AccJournalSignatureVw> AccJournalSignatureVws { get; }
        DbSet<AccJournalMasterExportVw> AccJournalMasterExportVws { get; }
        DbSet<AccBankVw> AccBankVws { get; }
        DbSet<AccAccountsLevel> AccAccountsLevels { get; }
        DbSet<AccPettyCashExpensesType> AccPettyCashExpensesTypes { get; }
        DbSet<AccPettyCashExpensesTypeVw> AccPettyCashExpensesTypeVws { get; }
        DbSet<AccBankChequeBook> AccBankChequeBooks { get; }
        DbSet<AccCashOnHandVw> AccCashOnHandVws { get; }    
        DbSet<AccAccountsGroupsFinalVw> AccAccountsGroupsFinalVws { get; }
        DbSet<AccAccountsCostcenterVw> AccAccountsCostcenterVws { get; }
        DbSet<AccReferenceTypeVw> AccReferenceTypeVws { get; }

        //public DbSet<AccBank> AccBanks  { get; }
        #endregion --------- End ACC --------------------------------------------------------------------------
        
        #region ======= PM ========================

        DbSet<PMProjects> PMProjects { get; }
        DbSet<PmProjectsVw> PmProjectsVws { get; }
        DbSet<PmProjectsType> PmProjectsTypes { get; }

        DbSet<PmProjectsTypeVw> PmProjectsTypeVws { get; }
        DbSet<PmExtractAdditionalType> PmExtractAdditionalTypes { get; }
        DbSet<PmExtractAdditionalTypeVw> PmExtractAdditionalTypeVws { get; }
        DbSet<PmProjectsStage> PmProjectsStages { get; }
        DbSet<PmProjectsStagesVw> PmProjectsStagesVws { get; }
        DbSet<PmProjectsStaff> PmProjectsStaffs { get; }
        DbSet<PmProjectsStaffVw> PmProjectsStaffVws { get; }
        DbSet<PmProjectStatusVw> PmProjectStatusVws { get; }

        /* DbSet<PmProjectsVw> PmProjectsVws { get; }

         DbSet<PmProjectPlan> PmProjectPlans { get;}
         DbSet<PmProjectPlansVw> PmProjectPlansVws { get; }
         DbSet<PmProjectsAddDeduc> PmProjectsAddDeducs { get;}
         DbSet<PmProjectsAddDeducVw> PmProjectsAddDeducVws { get; }
         DbSet<PmProjectsFile> PmProjectsFiles { get; }
         DbSet<PmProjectsFilesVw> PmProjectsFilesVws { get; }
         DbSet<PmProjectsInstallment> PmProjectsInstallments { get;  }
         DbSet<PmProjectsInstallmentVw> PmProjectsInstallmentVws { get; }
         DbSet<PmProjectsInstallmentAction> PmProjectsInstallmentActions { get; }
         DbSet<PmProjectsInstallmentActionVw> PmProjectsInstallmentActionVws { get; }
         DbSet<PmProjectsInstallmentPayment> PmProjectsInstallmentPayments { get;  }
         DbSet<PmProjectsInstallmentPaymentVw> PmProjectsInstallmentPaymentVws { get;  }
         DbSet<PmProjectsItem> PmProjectsItems { get;  }
         DbSet<PmProjectsItemsVw> PmProjectsItemsVws { get;  }
         DbSet<PmProjectsRisk> PmProjectsRisks { get;  }
         DbSet<PmProjectsRisksVw> PmProjectsRiskVws { get; }
         DbSet<PmProjectsRisksVw2> PmProjectsRisksVw2s { get; }

         DbSet<PmProjectsStaffType> PmProjectsStaffTypes { get;  }

         DbSet<PmProjectsStokeholder> PmProjectsStokeholders { get; }
         DbSet<PmProjectsStokeholderVw> PmProjectsStokeholderVws { get; }
       

         DbSet<PmProjectStatusVw> PmProjectStatusVws { get; }*/


        #endregion ======= PM ========================

        #region ============ RPT ==================
        DbSet<RptReport> RptReports { get; }
        DbSet<RptCustomReport> RptCustomReports { get; }
        #endregion --------- End RPT ----------------
        //=======================Gb=========

        #region ==========SAL==========================================
        DbSet<SalTransaction> SalTransactions { get; }
        DbSet<SalTransactionsVw> SalTransactionsVws { get; }

        DbSet<SalItemsPriceM> SalItemsPriceMs { get; }
        DbSet<SalItemsPriceMVw> SalItemsPriceMVws { get; }

        DbSet<SalPosSetting> SalPosSettings { get; }
        DbSet<SalPosSettingVw> SalPosSettingVws { get; }
        
        DbSet<SalPosUser> SalPosUsers { get; }
        DbSet<SalPosUsersVw> SalPosUsersVws { get; }
        DbSet<SalPaymentTerm> SalPaymentTerms { get; }
        DbSet<SalSetting> SalSetting { get; }
        DbSet<SalTransactionsType> SalTransactionsType { get; }
        DbSet<SalTransactionsCommission> SalTransactionsCommissions { get; }
        DbSet<SalTransactionsCommissionVw> SalTransactionsCommissionVws { get; }

        #endregion ==========End SAL===================================

        #region =======OPM=====================================
        DbSet<OpmContractLocation> OpmContractLocations { get; }
        DbSet<OpmContract> OpmContracts { get; }
        DbSet<OpmContractVw> OpmContractVws { get; }
        DbSet<OpmTransactionsItem> OpmTransactionsItems { get; }
        DbSet<OpmTransactionsLocation> OpmTransactionsLocations { get; }
        DbSet<OpmPolicy> OpmPolicies { get; }
        DbSet<OpmContractItem> OpmContractItems { get; }
        DbSet<OPMTransactionsDetailsVw> oPMTransactionsDetailsVws { get; }

        DbSet<OpmContarctEmp> OpmContarctEmps { get; }
        DbSet<OpmContarctAssign> OpmContarctAssigns { get; }
        DbSet<OpmContractVw> OpmContractVw { get; }

        DbSet<OpmContractReplaceEmp> OpmContractReplaceEmps { get; }
        DbSet<OpmContarctEmpVw> OpmContarctEmpVws { get; }
        DbSet<OpmContractItemsVw> OpmContractItemsVws { get; }
        DbSet<OPMPayrollD> OPMPayrollDs { get; }
        DbSet<OpmPURTransactionsDetails>  OpmPURTransactionsDetails { get; }
        DbSet<OpmPurTransactionsDetailsVw> OpmPurtransactionsDetailsVws { get; }


        DbSet<OpmContractReplaceEmpVw> OpmContractReplaceEmpVws { get; }
        DbSet<OPMPayrollDVW> OPMPayrollDVWs { get; }    
        DbSet<OPMContractLocationVW> oPMContractLocationVWs { get; }
        DbSet<OpmServicesTypesVW> opmServicesTypesVWs { get; }

        DbSet<OpmContarctEmpPopUpVw> OpmContarctEmpPopUpVws { get; }
        #endregion
        
        #region =======PUR=====================================

        DbSet<PURTransactions> pURTransactions { get; }
        DbSet<PURTransactionsVw> PURTransactionsVws { get; }  
        DbSet<PurTransactionsType> purTransactionsTypes { get; }

        #endregion

        #region =========WF==================================
        DbSet<WfAppGroup> WfAppGroups { get; }
        DbSet<WfAppGroupsVw> WfAppGroupsVws { get; }

        DbSet<WfAppType> WfAppTypes { get; }
        DbSet<WfAppTypeVw> WfAppTypeVws { get; }

        DbSet<WfAppTypeTable> WfAppTypeTables { get; }
        DbSet<WfLookUpCatagory> WfLookUpCatagories { get; }
        DbSet<WfStepsTransactionsVw> WfStepsTransactionsVws { get; }
        DbSet<WfStepsTransaction> WfStepsTransactions { get; }
        DbSet<WfStepsVw> WfStepsVws { get; }
        DbSet<WfStep> WfSteps { get; }
        DbSet<WfApplicationsStatus> WfApplicationsStatuss { get; }
        DbSet<WfApplicationsStatusVw> WfApplicationsStatusVws { get; }
        DbSet<WfApplication> WfApplications { get; }
        DbSet<WfApplicationsVw> WfApplicationsVws { get; }
        DbSet<WfDynamicValue> WfDynamicValues { get; }
        DbSet<WfStatus> WfStatuss { get; }
        DbSet<WfStepsNotification> WfStepsNotifications { get; }
        DbSet<SysScreenWorkflow> SysScreenWorkflow { get; }

        #endregion

        #region ======= WH ========================

        DbSet<WhUnit> WhUnits { get; }

        #endregion ======= WH ========================

        #region =============== FXA ===========================
        DbSet<FxaAdditionsExclusion> FxaAdditionsExclusions { get; }
        DbSet<FxaAdditionsExclusionVw> FxaAdditionsExclusionVws { get; }

        DbSet<FxaAdditionsExclusionType> FxaAdditionsExclusionTypes { get; }
        DbSet<FxaDepreciationMethod> FxaDepreciationMethods { get; }

        DbSet<FxaFixedAsset> FxaFixedAssets { get; }
        DbSet<FxaFixedAssetVw> FxaFixedAssetVws { get; }
        DbSet<FxaFixedAssetVw2> FxaFixedAssetVw2s { get; }

        DbSet<FxaFixedAssetTransfer> FxaFixedAssetTransfers { get; }
        DbSet<FxaFixedAssetTransferVw> FxaFixedAssetTransferVws { get; }

        DbSet<FxaFixedAssetType> FxaFixedAssetTypes { get; }
        DbSet<FxaFixedAssetTypeVw> FxaFixedAssetTypeVws { get; }

        DbSet<FxaTransaction> FxaTransactions { get; }
        DbSet<FxaTransactionsVw> FxaTransactionsVws { get; }

        DbSet<FxaTransactionsAssest> FxaTransactionsAssests { get; }
        DbSet<FxaTransactionsAssestVw> FxaTransactionsAssestVws { get; }

        DbSet<FxaTransactionsPayment> FxaTransactionsPayments { get; }

        DbSet<FxaTransactionsType> FxaTransactionsTypes { get; }

        DbSet<FxaTransactionsProduct> FxaTransactionsProducts { get; }
        DbSet<FxaTransactionsProductsVw> FxaTransactionsProductsVws { get; }

        DbSet<FxaTransactionsRevaluation> FxaTransactionsRevaluations { get; }
        DbSet<FxaTransactionsRevaluationVw> FxaTransactionsRevaluationVws { get; }
        #endregion

        #region =============== CRM ===========================

        DbSet<CrmEmailTemplateAttach> CrmEmailTemplateAttachs { get; }
        DbSet<CrmEmailTemplate> CrmEmailTemplates { get; }
        
        #endregion


        DbSet<BudgTransaction> BudgTransactions { get; }
        DbSet<BudgTransactionVw> BudgTransactionVws { get; }
        DbSet<BudgTransactionDetaile> BudgTransactionDetaile { get; }
        DbSet<BudgTransactionDetailesVw> BudgTransactionDetailesVws { get; }
        DbSet<BudgExpensesLinks> BudgExpensesLinks { get; }
        DbSet<BudgExpensesLinksVW> budgExpensesLinksVWs { get; }
        DbSet<OPMPayroll> oPMPayrolls { get; }
        DbSet<OpmPayrollVw> opmPayrollVws { get; }
        DbSet<OpmServicesTypes> OpmServicesTypes { get; }
        DbSet<OPMTransactionsDetails> OPMTransactionsDetails { get; }
        DbSet<BudgDocType> BudgDocType { get; }

        DbSet<BudgAccountExpenses> BudgAccountExpenses { get; }
        DbSet<BudgAccountExpensesVW> BudgAccountExpensesVW { get; }
        DbSet<BudgTransactionBalanceVW> BudgTransactionBalanceVW { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}