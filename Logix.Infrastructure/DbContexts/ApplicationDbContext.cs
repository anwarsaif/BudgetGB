using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Domain.ACC;
using Logix.Domain.Base;
using Logix.Domain.CRM;
using Logix.Domain.FXA;
using Logix.Domain.Gb;
using Logix.Domain.GB;
using Logix.Domain.HOT;
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
using Logix.Infrastructure.EntityConfigurations.ACC;
using Logix.Infrastructure.EntityConfigurations.CRM;
using Logix.Infrastructure.EntityConfigurations.FXA;
using Logix.Infrastructure.EntityConfigurations.GB;
using Logix.Infrastructure.EntityConfigurations.HOT;
using Logix.Infrastructure.EntityConfigurations.HR;
using Logix.Infrastructure.EntityConfigurations.Main;
using Logix.Infrastructure.EntityConfigurations.OPM;
using Logix.Infrastructure.EntityConfigurations.PM;
using Logix.Infrastructure.EntityConfigurations.PUR;
using Logix.Infrastructure.EntityConfigurations.RPT;
using Logix.Infrastructure.EntityConfigurations.SAL;
using Logix.Infrastructure.EntityConfigurations.WA;
using Logix.Infrastructure.EntityConfigurations.WF;
using Logix.Infrastructure.EntityConfigurations.WH;
using Microsoft.EntityFrameworkCore;

namespace Logix.Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ISessionHelper session;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ISessionHelper session) : base(options)
        {
            this.session = session;
        }


        /*   Each region is for one system entities   */

        #region ============ Main ===========================================================================
        public DbSet<SysCalendar> SysCalendars { get; private set; }
        public DbSet<SysNotificationsSetting> SysNotificationsSettings { get; private set; }
        public DbSet<SysNotificationsSettingVw> SysNotificationsSettingVws { get; private set; }
        public DbSet<SysSystem> SysSystems { get; private set; }
        public DbSet<SysScreen> SysScreens { get; private set; }
        public DbSet<SysAnnouncement> SysAnnouncements { get; private set; }
        public DbSet<SysAnnouncementVw> SysAnnouncementVws { get; private set; }
        public DbSet<SysAnnouncementLocationVw> SysAnnouncementLocationVws { get; private set; }
        public DbSet<SysLookupCategory> SysLookupCategories { get; private set; }
        public DbSet<SysLookupData> SysLookupData { get; private set; }
        public DbSet<SysLookupDataVw> SysLookupDataVws { get; private set; }
        public DbSet<SysDepartment> SysDepartments { get; private set; }
        public DbSet<SysDepartmentVw> SysDepartmentVws { get; private set; }
        public DbSet<SysDepartmentCatagory> SysDepartmentCatagories { get; private set; }
        public DbSet<SysGroup> SysGroups { get; private set; }
        public DbSet<SysGroupVw> SysGroupVws { get; private set; }
        public DbSet<SysScreenPermission> SysScreenPermissions { get; private set; }
        public DbSet<SysScreenPermissionVw> SysScreenPermissionVws { get; private set; }
        public DbSet<MainListDto> MainListDtos { get; private set; }
        public DbSet<HrEmployeeCostReportDto> HrEmployeeCostReportDtos { get; private set; }
        public DbSet<SubListDto> SubListDtos { get; private set; }
        public DbSet<SysBranchVw> SysBranchVws { get; private set; }
        public DbSet<SysNotification> SysNotifications { get; private set; }
        public DbSet<SysNotificationsVw> SysNotificationsVws { get; private set; }
        public DbSet<SysProperty> SysProperties { get; private set; }
        public DbSet<SysPropertiesVw> SysPropertiesVws { get; private set; }
        public DbSet<SysPropertyValue> SysPropertyValues { get; private set; }
        public DbSet<SysPropertyValuesVw> SysPropertyValuesVws { get; private set; }
        public DbSet<SysScreenProperty> SysScreenProperties { get; private set; }
        public DbSet<SysScreenPermissionProperty> SysScreenPermissionProperties { get; private set; }
        public DbSet<SysScreenPermissionPropertiesVw> SysScreenPermissionPropertiesVws { get; private set; }

        public DbSet<SysCustomerType> SysCustomerTypes { get; private set; }
        public DbSet<SysCustomerGroup> SysCustomerGroups { get; private set; }
        public DbSet<SysCustomerGroupAccount> SysCustomerGroupAccounts { get; private set; }
        public DbSet<SysCustomerGroupAccountsVw> SysCustomerGroupAccountsVws { get; private set; }
        public DbSet<SysLicense> SysLicenses { get; private set; }
        public DbSet<SysLicensesVw> SysLicensesVws { get; private set; }
        public DbSet<SysFavMenu> SysFavMenus { get; private set; }
        public DbSet<SysFile> SysFiles { get; private set; }
        public DbSet<SysCustomer> SysCustomer { get; private set; }

        public DbSet<InvestBranch> InvestBranches { get; set; } = null!;
        public DbSet<InvestBranchVw> InvestBranceshVws { get; set; } = null!;
        public DbSet<SysCurrency> SysCurrency { get; private set; } = null!;
        public DbSet<SysCurrencyListVw> SysCurrencyListVws { get; private set; } = null!;

        public DbSet<SysExchangeRate> SysExchangeRates { get; private set; } = null!;
        public DbSet<SysExchangeRateVw> SysExchangeRatesVws { get; private set; } = null!;
        public DbSet<SysExchangeRateListVW> SysExchangeRateListsVws { get; private set; } = null!;

        public DbSet<SysUser> SysUsers { get; private set; }
        public DbSet<SysUserVw> SysUserVws { get; private set; }
        public DbSet<SysScreenInstalled> SysScreenInstalleds { get; private set; }
        public DbSet<SysScreenInstalledVw> SysScreenInstalledVws { get; private set; }

        public DbSet<SysTasksVw> SysTasksVws { get; private set; }

        public DbSet<SysCustomerBranch> SysCustomerBranches { get; private set; }
        public DbSet<SysCustomerBranchVw> SysCustomerBranchVws { get; private set; }
        public DbSet<SysCites> SysCites { get; private set; }
        public DbSet<SysCustomerCoType> SysCustomerCoTypes { get; private set; }

        public DbSet<SysPoliciesProcedure> SysPoliciesProcedures { get; private set; }
        public DbSet<SysPoliciesProceduresVw> SysPoliciesProceduresVws { get; private set; }
        public DbSet<SysVatGroup> SysVatGroups { get; private set; }
        public DbSet<SysCustomerVw> SysCustomerVws { get; private set; }

        public DbSet<SysCustomerContact> SysCustomerContacts { get; private set; }
        public DbSet<SysCustomerContactVw> SysCustomerContactVws { get; private set; }
        public DbSet<SysCustomerFile> SysCustomerFiles { get; private set; }
        public DbSet<SysCustomerFilesVw> SysCustomerFilesVws { get; private set; }

        public DbSet<SysTemplate> SysTemplates { get; private set; }
        public DbSet<SysTemplateVw> SysTemplateVws { get; private set; }
        public DbSet<SysForm> SysForms { get; private set; }
        public DbSet<SysFormsVw> SysFormsVws { get; private set; }

        public DbSet<SysSettingExport> SysSettingExports { get; private set; }
        public DbSet<SysSettingExportVw> SysSettingExportVws { get; private set; }

        public DbSet<SysActivityLog> SysActivityLogs { get; private set; }
        public DbSet<SysActivityLogVw> SysActivityLogVws { get; private set; }

        public DbSet<Domain.Main.InvestEmployee> InvestEmployees { get; private set; }
        public DbSet<InvestEmployeeVvw> InvestEmployeeVvws { get; private set; }
        public DbSet<SysUserLogTime> SysUserLogTimes { get; private set; }
        public DbSet<SysUserLogTimeVw> SysUserLogTimeVws { get; private set; }

        public DbSet<SysUserTracking> SysUserTrackings { get; private set; }
        public DbSet<SysUserTrackingVw> SysUserTrackingVws { get; private set; }

        public DbSet<SysUserType> SysUserTypes { get; private set; }
        public DbSet<SysUserType2> SysUserTypes2 { get; private set; }

        public DbSet<SysVersion> SysVersions { get; private set; }

        public DbSet<SysDynamicAttribute> SysDynamicAttributes { get; private set; }
        public DbSet<SysDynamicAttributesVw> SysDynamicAttributesVws { get; private set; }
        public DbSet<SysDynamicAttributeDataType> SysDynamicAttributeDataTypes { get; private set; }
        public DbSet<SysScreenVw> SysScreenVws { get; private set; }
        public DbSet<SysVatGroupVw> SysVatGroupVws { get; private set; }
        public DbSet<MonthDay> MonthDays { get; private set; }
        public DbSet<SysMailServer> SysMailServer { get; private set; }
        public DbSet<SysCountry> SysCountrys { get; private set; }
        public DbSet<SysCountryVw> SysCountryVws { get; private set; }

        public DbSet<SysNotificationsMang> SysNotificationsMangs { get; private set; }
        public DbSet<SysNotificationsMangVw> SysNotificationsMangVws { get; private set; }

        public DbSet<SysTable> SysTables { get; private set; }
        public DbSet<SysTableField> SysTableFields { get; private set; }

        public DbSet<SysActivityType> SysActivityTypes { get; private set; }
        public DbSet<SysPackagesPropertyValue> SysPackagesPropertyValue { get; private set; }

        public DbSet<SysZatcaInvoiceType> SysZatcaInvoiceTypes { get; private set; }

        #endregion ------------------ End Main ---------------------------------------------------------------

        #region ======= PM ========================

        public DbSet<PmJobsSalary> pmJobsSalaries { get; private set; } = null!;
        public DbSet<PmJobsSalaryVw> PmJobsSalaryVws { get; private set; } = null!;
        public DbSet<PmOperationalControl> PmOperationalControls { get; private set; } = null!;
        public DbSet<PmOperationalControlsVw> PmOperationalControlsVws { get; private set; } = null!;
        public DbSet<PMProjects> PMProjects { get; private set; } = null!;
        public DbSet<PmProjectsVw> PmProjectsVws { get; private set; }
        public DbSet<PmExtractAdditionalType> PmExtractAdditionalTypes { get; private set; }
        public DbSet<PmExtractAdditionalTypeVw> PmExtractAdditionalTypeVws { get; private set; }

        public DbSet<PmProjectsStage> PmProjectsStages { get; private set; }
        public DbSet<PmProjectsStagesVw> PmProjectsStagesVws { get; private set; }
        public DbSet<PmProjectsStaff> PmProjectsStaffs { get; private set; }
        public DbSet<PmProjectsStaffVw> PmProjectsStaffVws { get; private set; }

        public DbSet<PmProjectPlan> PmProjectPlans { get; private set; }
        public DbSet<PmProjectPlansVw> PmProjectPlansVws { get; private set; }
        public DbSet<PmProjectsAddDeduc> PmProjectsAddDeducs { get; private set; }
        public DbSet<PmProjectsAddDeducVw> PmProjectsAddDeducVws { get; private set; }
        public DbSet<PmProjectsFile> PmProjectsFiles { get; private set; }
        public DbSet<PmProjectsFilesVw> PmProjectsFilesVws { get; private set; }
        public DbSet<PmProjectsInstallment> PmProjectsInstallments { get; private set; }
        public DbSet<PmProjectsInstallmentVw> PmProjectsInstallmentVws { get; private set; }
        public DbSet<PmProjectsInstallmentAction> PmProjectsInstallmentActions { get; private set; }
        public DbSet<PmProjectsInstallmentActionVw> PmProjectsInstallmentActionVws { get; private set; }
        public DbSet<PmProjectsInstallmentPayment> PmProjectsInstallmentPayments { get; private set; }
        public DbSet<PmProjectsInstallmentPaymentVw> PmProjectsInstallmentPaymentVws { get; private set; }
        public DbSet<PmProjectsItem> PmProjectsItems { get; private set; }
        public DbSet<PmProjectsItemsVw> PmProjectsItemsVws { get; private set; }
        public DbSet<PmProjectsRisk> PmProjectsRisks { get; private set; }
        public DbSet<PmProjectsRisksVw> PmProjectsRiskVws { get; private set; }
        public DbSet<PmProjectsRisksVw2> PmProjectsRisksVw2s { get; private set; }

        public DbSet<PmProjectsStaffType> PmProjectsStaffTypes { get; private set; }

        public DbSet<PmProjectsStokeholder> PmProjectsStokeholders { get; private set; }
        public DbSet<PmProjectsStokeholderVw> PmProjectsStokeholderVws { get; private set; }
        public DbSet<PmProjectsType> PmProjectsTypes { get; private set; }
        public DbSet<PmProjectsTypeVw> PmProjectsTypeVws { get; private set; }
        public DbSet<PmProjectStatusVw> PmProjectStatusVws { get; private set; }


        #endregion ======= PM ========================

        #region ============ RPT ==================
        public DbSet<RptReport> RptReports { get; private set; }
        public DbSet<RptCustomReport> RptCustomReports { get; private set; }
        #endregion --------- End RPT ----------------

        #region ============ HR ==============================================================================

        public DbSet<HrAllowanceVw> HrAllowanceVws { get; private set; }
        public DbSet<HrDeductionVw> HrDeductionVws { get; private set; }
        public DbSet<HrDisciplinaryActionType> HrDisciplinaryActionTypes { get; private set; }
        public DbSet<HrAttTimeTableDay> HrAttTimeTableDays { get; private set; }
        public DbSet<HrAttLocation> HrAttLocations { get; private set; }
        public DbSet<HrEmployee> HrEmployees { get; private set; }
        public DbSet<HrEmployeeVw> HrEmployeeVws { get; private set; }
        public DbSet<HrAttDay> HrAttDays { get; private set; }
        public DbSet<HrEvaluationAnnualIncreaseConfig> HrEvaluationAnnualIncreaseConfigs { get; private set; }
        public DbSet<HrNotification> HrNotifications { get; private set; }
        public DbSet<HrNotificationsVw> HrNotificationsVws { get; private set; }
        public DbSet<HrCompetence> HrCompetences { get; private set; }
        public DbSet<HrCompetencesVw> HrCompetencesVws { get; private set; }
        public DbSet<HrCompetencesCatagory> HrCompetencesCatagorys { get; private set; }
        public DbSet<HrKpiTemplatesCompetence> HrKpiTemplatesCompetences { get; private set; }
        public DbSet<HrTrainingBag> HrTrainingBags { get; private set; }
        public DbSet<HrTrainingBagVw> HrTrainingBagVws { get; private set; }

        public DbSet<HrPolicy> HrPolicys { get; private set; }
        public DbSet<HrPoliciesVw> HrPoliciesVws { get; private set; }

        public DbSet<HrPoliciesType> HrPoliciesTypes { get; private set; }

        public DbSet<HrKpiTemplate> HrKpiTemplates { get; private set; }
        public DbSet<HrKpiTemplatesVw> HrKpiTemplatesVws { get; private set; }
        public DbSet<HrSetting> HrSettings { get; private set; }
        public DbSet<HrSalaryGroup> HrSalaryGroups { get; private set; }
        public DbSet<HrSalaryGroupVw> HrSalaryGroupVws { get; private set; }
        public DbSet<HrVacationsType> HrVacationsTypes { get; private set; }
        public DbSet<HrVacationsTypeVw> HrVacationsTypeVws { get; private set; }
        public DbSet<HrDisciplinaryCase> HrDisciplinaryCases { get; private set; }
        public DbSet<HrAbsence> HrAbsences { get; private set; }
        public DbSet<HrDelay> HrDelays { get; private set; }
        public DbSet<HrNote> HrNotes { get; private set; }
        public DbSet<HrNoteVw> HrNoteVws { get; private set; }


        //public DbSet<HrDisciplinaryCase> HrDisciplinaryCases { get; private set; } 
        public DbSet<HrCardTemplate> HrCardTemplates { get; private set; }
        public DbSet<InvestMonth> investMonths { get; private set; }
        public DbSet<HrOverTimeD> hrOverTimeDs { get; private set; }
        public DbSet<HrOverTimeDVw> hrOverTimeDVws { get; private set; }

        public DbSet<HrAttShift> HrAttShifts { get; private set; }
        public DbSet<HrJobProgramVw> HrJobProgramVws { get; private set; }
        public DbSet<HrJobVw> HrJobVws { get; private set; }

        public DbSet<HrAttendanceReportDto> HrAttendanceReportDto { get; private set; }
        public DbSet<HrVacations> HrVacations { get; private set; }
        public DbSet<HrVacationsVw> HrVacationsVws { get; private set; }
        public DbSet<HrAttendance> HrAttendances { get; private set; }
        public DbSet<HrAttendancesVw> HrAttendancesVws { get; private set; }
        public DbSet<HrAttTimeTable> HrAttTimeTables { get; private set; }
        public DbSet<HrAttTimeTableVw> HrAttTimeTableVws { get; private set; }
        public DbSet<HrAttShiftEmployee> HrAttShiftEmployees { get; private set; }
        public DbSet<HrAttShiftEmployeeVw> HrAttShiftEmployeeVws { get; private set; }
        public DbSet<HrMandate> HrMandates { get; private set; }
        public DbSet<HrMandateVw> HrMandateVws { get; private set; }
        public DbSet<HrKpiType> HrKpiTypes { get; private set; }
        public DbSet<HrKpiTemplatesCompetencesVw> HrKpiTemplatesCompetencesVws { get; private set; }
        public DbSet<HrDisciplinaryCaseAction> HrDisciplinaryCaseActions { get; private set; }
        public DbSet<HrDisciplinaryCaseActionVw> HrDisciplinaryCaseActionVws { get; private set; }
        public DbSet<HrDisciplinaryRule> HrDisciplinaryRules { get; private set; }
        public DbSet<HrDisciplinaryRuleVw> HrDisciplinaryRuleVws { get; private set; }
        public DbSet<HrRateType> HrRateTypes { get; private set; }
        public DbSet<HrRateTypeVw> HrRateTypeVws { get; private set; }
        public DbSet<HrVacationsCatagory> HrVacationsCatagories { get; private set; }
        public DbSet<HrAllowanceDeduction> HrAllowanceDeductions { get; private set; }
        public DbSet<HrAllowanceDeductionVw> HrAllowanceDeductionVws { get; private set; }
        public DbSet<HrLoanVw> HrLoanVws { get; private set; }
        public DbSet<HrLoan> HrLoans { get; private set; }
        public DbSet<HrAbsenceVw> HrAbsenceVw { get; private set; }
        public DbSet<HrPayrollDVw> HrPayrollDVws { get; private set; }
        public DbSet<HrPayrollD> HrPayrollDs { get; private set; }
        public DbSet<HrArchivesFile> HrArchivesFiles { get; private set; }
        public DbSet<HrArchivesFilesVw> HrArchivesFilesVws { get; private set; }
        public DbSet<HrLicense> HrLicenses { get; private set; }
        public DbSet<HrLicensesVw> HrLicensesVws { get; private set; }
        public DbSet<HrTransfer> HrTransfers { get; private set; }
        public DbSet<HrTransfersVw> HrTransfersVws { get; private set; }
        public DbSet<HrOverTimeM> HrOverTimeMs { get; private set; }
        public DbSet<HrOverTimeMVw> HrOverTimeMVws { get; private set; }
        public DbSet<HrOhadDetail> HrOhadDetails { get; private set; }
        public DbSet<HrOhadDetailsVw> HrOhadDetailsVws { get; private set; }
        public DbSet<HrEmpWarn> HrEmpWarns { get; private set; }
        public DbSet<HrEmpWarnVw> HrEmpWarnVws { get; private set; }
        public DbSet<HrVacationBalance> HrVacationBalances { get; private set; }
        public DbSet<HrVacationBalanceVw> HrVacationBalanceVws { get; private set; }
        public DbSet<HrDependent> HrDependents { get; private set; }
        public DbSet<HrDependentsVw> HrDependentsVws { get; private set; }
        public DbSet<HrDirectJob> HrDirectJobs { get; private set; }
        public DbSet<HrDirectJobVw> HrDirectJobVws { get; private set; }
        public DbSet<HrIncrement> HrIncrements { get; private set; }
        public DbSet<HrIncrementsVw> HrIncrementsVws { get; private set; }
        public DbSet<HrLeave> HrLeaves { get; private set; }
        public DbSet<HrLeaveVw> HrLeaveVws { get; private set; }
        public DbSet<HrKpi> HrKpis { get; private set; }
        public DbSet<HrKpiVw> HrKpiVws { get; private set; }
        public DbSet<HrKpiDetaile> HrKpiDetailes { get; private set; }
        public DbSet<HrKpiDetailesVw> HrKpiDetailesVws { get; private set; }
        public DbSet<HrEmpWorkTime> HrEmpWorkTimes { get; private set; }
        public DbSet<HrEmpWorkTimeVw> HrEmpWorkTimeVws { get; private set; }
        public DbSet<HrSalaryGroupAccount> HrSalaryGroupAccounts { get; private set; }
        public DbSet<HrSalaryGroupRefrance> HrSalaryGroupRefrances { get; private set; }
        public DbSet<HrSalaryGroupRefranceVw> HrSalaryGroupRefranceVws { get; private set; }
        public DbSet<HrSalaryGroupAllowanceVw> HrSalaryGroupAllowanceVws { get; private set; }
        public DbSet<HrSalaryGroupDeductionVw> HrSalaryGroupDeductionVws { get; private set; }
        public DbSet<HrNotificationsTypeVw> HrNotificationsTypeVws { get; private set; }
        public DbSet<HrNotificationsType> HrNotificationsTypes { get; private set; }
        public DbSet<HrNotificationsSetting> HrNotificationsSettings { get; private set; }
        public DbSet<HrNotificationsSettingVw> HrNotificationsSettingVws { get; private set; }


        public DbSet<HrAllowanceDeductionM> HrAllowanceDeductionMs { get; private set; }
        public DbSet<HrAllowanceDeductionTempOrFix> HrAllowanceDeductionTempOrFixs { get; private set; }
        public DbSet<HrArchiveFilesDetail> HrArchiveFilesDetails { get; private set; }
        public DbSet<HrArchiveFilesDetailsVw> HrArchiveFilesDetailsVws { get; private set; }
        public DbSet<HrAssignman> HrAssignmans { get; private set; }
        public DbSet<HrAssignmenVw> HrAssignmenVws { get; private set; }
        public DbSet<HrAttAction> HrAttActions { get; private set; }
        public DbSet<HrAttLocationEmployee> HrAttLocationEmployees { get; private set; }
        public DbSet<HrAttLocationEmployeeVw> HrAttLocationEmployeeVws { get; private set; }
        public DbSet<HrAttShiftClose> HrAttShiftCloses { get; private set; }
        public DbSet<HrAttShiftCloseVw> HrAttShiftCloseVws { get; private set; }
        public DbSet<HrAttShiftCloseD> HrAttShiftCloseDs { get; private set; }
        public DbSet<HrAuthorization> HrAuthorizations { get; private set; }
        public DbSet<HrAuthorizationVw> HrAuthorizationVws { get; private set; }
        public DbSet<HrAttendanceBioTime> HrAttendanceBioTimes { get; private set; }
        public DbSet<HrCheckInOut> HrCheckInOuts { get; private set; }
        public DbSet<HrCheckInOutVw> HrCheckInOutVws { get; private set; }

        public DbSet<HrClearance> HrClearances { get; private set; }
        public DbSet<HrClearanceVw> HrClearanceVws { get; private set; }
        public DbSet<HrClearanceType> HrClearanceTypes { get; private set; }
        public DbSet<HrClearanceTypeVw> HrClearanceTypeVws { get; private set; }

        public DbSet<HrCompensatoryVacation> HrCompensatoryVacations { get; private set; }
        public DbSet<HrCompensatoryVacationsVw> HrCompensatoryVacationsVws { get; private set; }


        public DbSet<HrOhad> HrOhads { get; private set; }
        public DbSet<HrOhadVw> HrOhadVws { get; private set; }
        public DbSet<HrContracte> HrContractes { get; private set; }
        public DbSet<HrContractesVw> HrContractesVws { get; private set; }
        public DbSet<HrClearanceMonth> HrClearanceMonths { get; private set; }

        public DbSet<HrRequest> HrRequests { get; private set; }
        public DbSet<HrRequestVw> HrRequestVws { get; private set; }
        public DbSet<HrRequestDetaile> HrRequestDetailes { get; private set; }
        public DbSet<HrRequestDetailesVw> HrRequestDetailesVws { get; private set; }
        public DbSet<HrRequestType> HrRequestTypes { get; private set; }



        public DbSet<HrCostType> HrCostTypes { get; private set; }
        public DbSet<HrCostTypeVw> HrCostTypeVws { get; private set; }
        public DbSet<HrCustody> HrCustodys { get; private set; }
        public DbSet<HrCustodyVw> HrCustodyVws { get; private set; }
        public DbSet<HrCustodyItem> HrCustodyItems { get; private set; }
        public DbSet<HrCustodyItemsVw> HrCustodyItemsVws { get; private set; }
        public DbSet<HrCustodyItemsProperty> HrCustodyItemsPropertys { get; private set; }
        public DbSet<HrCustodyRefranceType> HrCustodyRefranceTypes { get; private set; }
        public DbSet<HrCustodyType> HrCustodyTypes { get; private set; }
        public DbSet<HrDecision> HrDecisions { get; private set; }
        public DbSet<HrDecisionsVw> HrDecisionsVws { get; private set; }
        public DbSet<HrDecisionsEmployee> HrDecisionsEmployees { get; private set; }
        public DbSet<HrDecisionsEmployeeVw> HrDecisionsEmployeeVws { get; private set; }
        public DbSet<HrIncrementsAllowanceDeduction> HrIncrementsAllowanceDeductions { get; private set; }
        public DbSet<HrDelayVw> HrDelayVws { get; private set; }
        public DbSet<HrHoliday> HrHolidays { get; private set; }
        public DbSet<HrHolidayVw> HrHolidayVws { get; private set; }
        public DbSet<HrPermission> HrPermissions { get; private set; }
        public DbSet<HrPermissionsVw> HrPermissionsVws { get; private set; }

        public DbSet<HrAttShiftTimeTable> HrAttShiftTimeTables { get; private set; }
        public DbSet<HrAttShiftTimeTableVw> HrAttShiftTimeTableVws { get; private set; }
        public DbSet<HrEmployeeCost> HrEmployeeCosts { get; private set; }
        public DbSet<HrEmployeeCostVw> HrEmployeeCostVws { get; private set; }

        public DbSet<HrInsurancePolicy> HrInsurancePolicys { get; private set; }
        public DbSet<HrInsurance> HrInsurances { get; private set; }
        public DbSet<HrInsuranceEmp> HrInsuranceEmps { get; private set; }
        public DbSet<HrInsuranceEmpVw> HrInsuranceEmpVws { get; private set; }

        public DbSet<HrJob> HrJobs { get; private set; }
        public DbSet<HrJobDescription> HrJobDescriptions { get; private set; }
        public DbSet<HrJobEmployeeVw> HrJobEmployeeVws { get; private set; }
        public DbSet<HrJobLevel> HrJobLevels { get; private set; }
        public DbSet<HrRecruitmentVacancy> HrRecruitmentVacancys { get; private set; }
        public DbSet<HrRecruitmentVacancyVw> HrRecruitmentVacancyVws { get; private set; }
        public DbSet<HrRecruitmentApplication> HrRecruitmentApplications { get; private set; }
        public DbSet<HrRecruitmentApplicationVw> HrRecruitmentApplicationVws { get; private set; }
        public DbSet<HrRecruitmentCandidate> HrRecruitmentCandidates { get; private set; }
        public DbSet<HrRecruitmentCandidateVw> HrRecruitmentCandidateVws { get; private set; }


        public DbSet<HrVacancyStatusVw> HrVacancyStatusVws { get; private set; }
        public DbSet<HrJobGrade> HrJobGrades { get; private set; }
        public DbSet<HrJobGradeVw> HrJobGradeVws { get; private set; }
        public DbSet<HrRecruitmentCandidateKpi> HrRecruitmentCandidateKpis { get; private set; }
        public DbSet<HrRecruitmentCandidateKpiVw> HrRecruitmentCandidateKpiVws { get; private set; }
        public DbSet<HrRecruitmentCandidateKpiD> HrRecruitmentCandidateKpiDs { get; private set; }
        public DbSet<HrRecruitmentCandidateKpiDVw> HrRecruitmentCandidateKpiDVws { get; private set; }
        public DbSet<HrPayroll> HrPayrolls { get; private set; }
        public DbSet<HrPayrollVw> HrPayrollVws { get; private set; }
        public DbSet<HrTicketVw> HrTicketVws { get; private set; }
        public DbSet<HrTicket> HrTickets { get; private set; }
        public DbSet<HrVisaVw> HrVisaVws { get; private set; }
        public DbSet<HrVisa> HrVisas { get; private set; }
        public DbSet<HrVacationEmpBalanceDto> HrVacationEmpBalanceDtos { get; private set; }
        public DbSet<HrVacationBalanceALLFilterDto> HrVacationBalanceALLFilterDtos { get; private set; }
        public DbSet<HrFixingEmployeeSalary> HrFixingEmployeeSalarys { get; private set; }
        public DbSet<HrFixingEmployeeSalaryVw> HrFixingEmployeeSalaryVws { get; private set; }
        public DbSet<HrLeaveType> HrLeaveTypes { get; private set; }
        public DbSet<HrLeaveTypeVw> HrLeaveTypeVws { get; private set; }
        public DbSet<HrPayrollAllowanceDeduction> HrPayrollAllowanceDeductions { get; private set; }
        public DbSet<HrPayrollAllowanceDeductionVw> HrPayrollAllowanceDeductionVws { get; private set; }
        public DbSet<HrLoanInstallmentPayment> HrLoanInstallmentPayments { get; private set; }
        public DbSet<HrLoanInstallmentPaymentVw> HrLoanInstallmentPaymentVws { get; private set; }
        public DbSet<HrLoanInstallment> HrLoanInstallments { get; private set; }
        public DbSet<HrLoanInstallmentVw> HrLoanInstallmentVws { get; private set; }

        public DbSet<HrPayrollNote> HrPayrollNotes { get; private set; }

        public DbSet<HrPayrollNoteVw> HrPayrollNoteVws { get; private set; }
        public DbSet<HrDecisionsTypeVw> HrDecisionsTypeVws { get; private set; }
        public DbSet<HrDecisionsType> HrDecisionsTypes { get; private set; }
        public DbSet<HrDecisionsTypeEmployee> HrDecisionsTypeEmployees { get; private set; }
        public DbSet<HrDecisionsTypeEmployeeVw> HrDecisionsTypeEmployeeVws { get; private set; }

        #endregion --------- End HR --------------------------------------------------------------------------

        #region ============ ACC ==============================================================================

        public DbSet<AccAccount> AccAccounts { get; private set; }
        public DbSet<AccFinancialYear> AccFinancialYears { get; private set; }
        public DbSet<AccFacility> AccFacilities { get; private set; }
        public DbSet<AccFacilitiesVw> AccFacilitiesVws { get; private set; }
        public DbSet<AccGroup> AccGroup { get; private set; }
        public DbSet<AccPeriods> AccPeriods { get; private set; }
        public DbSet<AccCostCenterVws> AccCostCenterVws { get; private set; }
        public DbSet<AccJournalMaster> AccJournalMasters { get; private set; }
        public DbSet<AccJournalMasterVw> AccJournalMasterVws { get; private set; }
        public DbSet<AccCostCenter> AccCostCenter { get; private set; }

        public DbSet<AccReferenceType> AccReferenceTypes { get; private set; }
        public DbSet<AccAccountsVw> AccAccountsVw { get; private set; }
        public DbSet<WhItemsVw> WhItemsVws { get; private set; }

        public DbSet<AccBranchAccount> AccBranchAccounts { get; private set; }
        public DbSet<AccBranchAccountsVw> AccBranchAccountsVws { get; private set; }
        public DbSet<AccBranchAccountType> AccBranchAccountTypes { get; private set; }

        public DbSet<AccPeriodDateVws> AccPeriodDateVws { get; private set; }
        public DbSet<AccJournalDetaile> AccJournalDetailes { get; private set; }
        public DbSet<AccJournalDetailesVw> AccJournalDetailesVws { get; private set; }
        public DbSet<AccAccountsCloseType> AccAccountsCloseTypes { get; private set; }
        public DbSet<AccBank> AccBanks { get; private set; }
        public DbSet<AccCashOnHand> AccCashOnHands { get; private set; }
        public DbSet<AccRequest> AccRequests { get; private set; }
        public DbSet<AccRequestVw> AccRequestVws { get; private set; }
        public DbSet<AccDocumentTypeListVw> AccDocumentTypeListVws { get; private set; }
        public DbSet<AccCashOnHandListVw> AccCashonhandListVWs { get; private set; }
        public DbSet<AccAccountsSubHelpeVw> AccAccountsSubHelpeVws { get; private set; }
        public DbSet<AccCostCenteHelpVw> AccCostCenteHelpVws { get; private set; }
        public DbSet<AccJournalSignatureVw> AccJournalSignatureVws { get; private set; }
        public DbSet<AccJournalMasterExportVw> AccJournalMasterExportVws { get; private set; }
        public DbSet<AccBankVw> AccBankVws { get; private set; }
        public DbSet<AccAccountsLevel> AccAccountsLevels { get; private set; }
        public DbSet<AccPettyCashExpensesType> AccPettyCashExpensesTypes { get; private set; }
        public DbSet<AccPettyCashExpensesTypeVw> AccPettyCashExpensesTypeVws { get; private set; }
        public DbSet<AccBankChequeBook> AccBankChequeBooks { get; private set; }
        public DbSet<AccCashOnHandVw> AccCashOnHandVws { get; private set; }
        public DbSet<AccAccountsGroupsFinalVw> AccAccountsGroupsFinalVws { get; private set; }
        public DbSet<AccAccountsCostcenterVw> AccAccountsCostcenterVws { get; private set; }
        public DbSet<AccReferenceTypeVw> AccReferenceTypeVws { get; private set; }


        //public DbSet<AccBank> AccBanks { get; private set; }
        #endregion --------- End ACC --------------------------------------------------------------------------

        #region ============ WhatsApp ==============================================================================
        public DbSet<WaWhatsappSetting> WaWhatsappSettings { get; private set; }
        public DbSet<WaTemplateMessageValue> WaTemplateMessageValues { get; }
        public DbSet<WaTemplateMessage> WaTemplateMessages { get; }
        public DbSet<WaDirectMessage> WaDirectMessages { get; }
        #endregion --------- End WhatsApp --------------------------------------------------------------------------

        #region //=============================Gb======================

        public DbSet<BudgTransaction> BudgTransactions { get; private set; }
        public DbSet<BudgTransactionVw> BudgTransactionVws { get; private set; }
        public DbSet<BudgTransactionDetaile> BudgTransactionDetaile { get; private set; }
        public DbSet<BudgTransactionDetailesVw> BudgTransactionDetailesVws { get; private set; }
        public DbSet<BudgExpensesLinks> BudgExpensesLinks { get; private set; }
        public DbSet<BudgExpensesLinksVW> budgExpensesLinksVWs { get; private set; }
        public DbSet<BudgDocType> BudgDocType { get; private set; }
        public DbSet<BudgAccountExpenses> BudgAccountExpenses { get; private set; }
        public DbSet<BudgAccountExpensesVW> BudgAccountExpensesVW { get; private set; }
        public DbSet<BudgTransactionBalanceVW> BudgTransactionBalanceVW { get; private set; }


        #endregion==================End GB====================

        #region ================SAL=====================================================
        public DbSet<SalTransaction> SalTransactions { get; private set; }
        public DbSet<SalTransactionsVw> SalTransactionsVws { get; private set; }

        public DbSet<SalItemsPriceM> SalItemsPriceMs { get; private set; }
        public DbSet<SalItemsPriceMVw> SalItemsPriceMVws { get; private set; }

        public DbSet<SalPosSetting> SalPosSettings { get; private set; }
        public DbSet<SalPosSettingVw> SalPosSettingVws { get; private set; }

        public DbSet<SalPosUser> SalPosUsers { get; private set; }
        public DbSet<SalPosUsersVw> SalPosUsersVws { get; private set; }
        public DbSet<SalPaymentTerm> SalPaymentTerms { get; private set; }
        public DbSet<SalSetting> SalSetting { get; private set; }
        public DbSet<SalTransactionsType> SalTransactionsType { get; private set; }
        public DbSet<SalTransactionsCommission> SalTransactionsCommissions { get; private set; }
        public DbSet<SalTransactionsCommissionVw> SalTransactionsCommissionVws { get; private set; }

        #endregion =============End SAL=================================================

        #region ================OPM=====================================================
        public DbSet<OpmContractLocation> OpmContractLocations { get; private set; }
        public DbSet<OpmContract> OpmContracts { get; private set; }
        public DbSet<OpmContractVw> OpmContractVws { get; private set; }
        public DbSet<OpmTransactionsItem> OpmTransactionsItems { get; private set; }
        public DbSet<OpmTransactionsLocation> OpmTransactionsLocations { get; private set; }
        public DbSet<OpmPolicy> OpmPolicies { get; private set; }

        public DbSet<OpmContractItem> OpmContractItems { get; private set; }
        public DbSet<OpmContarctEmp> OpmContarctEmps { get; private set; }
        public DbSet<OpmContarctAssign> OpmContarctAssigns { get; private set; }
        public DbSet<OpmContractVw> OpmContractVw { get; private set; }

        public DbSet<OpmContractReplaceEmp> OpmContractReplaceEmps { get; private set; }
        public DbSet<OpmContarctEmpVw> OpmContarctEmpVws { get; private set; }
        public DbSet<OpmContractItemsVw> OpmContractItemsVws { get; }
        public DbSet<OPMPayrollD> OPMPayrollDs { get; private set; }
        public DbSet<OPMPayroll> oPMPayrolls { get; private set; }
        public DbSet<OpmPayrollVw> opmPayrollVws { get; private set; }
        public DbSet<HrPayrollType> HrPayrollTypes { get; private set; }
        public DbSet<OPMTransactionsDetails> OPMTransactionsDetails { get; private set; }
        public DbSet<OpmPURTransactionsDetails> OpmPURTransactionsDetails { get; private set; }
        public DbSet<OpmPurTransactionsDetailsVw> OpmPurtransactionsDetailsVws { get; private set; }

        public DbSet<OpmContractReplaceEmpVw> OpmContractReplaceEmpVws { get; private set; }
        public DbSet<OPMPayrollDVW> OPMPayrollDVWs { get; private set; }
        public DbSet<OPMContractLocationVW> oPMContractLocationVWs { get; private set; }
        public DbSet<OpmServicesTypes> OpmServicesTypes { get; private set; }
        public DbSet<OpmServicesTypesVW> opmServicesTypesVWs { get; private set; }
        public DbSet<OPMTransactionsDetailsVw> oPMTransactionsDetailsVws { get; private set; }

        public DbSet<OpmContarctEmpPopUpVw> OpmContarctEmpPopUpVws { get; private set; }
        #endregion
        //================PUR--------------------
        #region =======PUR=====================================

        public DbSet<PURTransactions> pURTransactions { get; private set; }
        public DbSet<PURTransactionsVw> PURTransactionsVws { get; private set; }
        public DbSet<PurTransactionsType> purTransactionsTypes { get; private set; }
        #endregion {

        #region ========WF========================================================
        public DbSet<WfAppGroup> WfAppGroups { get; private set; }
        public DbSet<WfAppGroupsVw> WfAppGroupsVws { get; private set; }

        public DbSet<WfAppType> WfAppTypes { get; private set; }
        public DbSet<WfAppTypeVw> WfAppTypeVws { get; private set; }

        public DbSet<WfLookUpCatagory> WfLookUpCatagories { get; private set; }
        public DbSet<WfAppTypeTable> WfAppTypeTables { get; private set; }
        public DbSet<WfStepsTransactionsVw> WfStepsTransactionsVws { get; private set; }
        public DbSet<WfStepsTransaction> WfStepsTransactions { get; private set; }

        public DbSet<WfStepsVw> WfStepsVws { get; private set; }
        public DbSet<WfStep> WfSteps { get; private set; }
        public DbSet<WfApplication> WfApplications { get; private set; }
        public DbSet<WfApplicationsStatus> WfApplicationsStatuss { get; private set; }
        public DbSet<WfApplicationsStatusVw> WfApplicationsStatusVws { get; private set; }
        public DbSet<WfApplicationsVw> WfApplicationsVws { get; private set; }
        public DbSet<WfDynamicValue> WfDynamicValues { get; private set; }
        public DbSet<WfStatus> WfStatuss { get; private set; }
        public DbSet<WfStepsNotification> WfStepsNotifications { get; private set; }
        public DbSet<SysScreenWorkflow> SysScreenWorkflow { get; private set; }


        #endregion

        #region ========= Hot ==========
        public DbSet<HotFloor> HotFloors { get; private set; }
        public DbSet<HotFloorsVw> HotFloorsVws { get; private set; }
        public DbSet<HotGroup> HotGroups { get; private set; }
        public DbSet<HotGroupsVw> HotGroupsVws { get; private set; }
        public DbSet<HotRoom> HotRooms { get; private set; }
        public DbSet<HotRoomAsset> HotRoomAssets { get; private set; }
        public DbSet<HotRoomAssetsVw> HotRoomAssetsVws { get; private set; }
        public DbSet<HotRoomService> HotRoomServices { get; private set; }
        public DbSet<HotRoomVw> HotRoomVws { get; private set; }
        public DbSet<HotServicesVw> HotServicesVws { get; private set; }
        public DbSet<HotTransaction> HotTransactions { get; private set; }
        public DbSet<HotTransactionsCompanion> HotTransactionsCompanions { get; private set; }
        public DbSet<HotTransactionsCompanionVw> HotTransactionsCompanionVws { get; private set; }
        public DbSet<HotTransactionsPayment> HotTransactionsPayments { get; private set; }
        public DbSet<HotTransactionsRoom> HotTransactionsRooms { get; private set; }
        public DbSet<HotTransactionsRoomVw> HotTransactionsRoomVws { get; private set; }
        public DbSet<HotTransactionsService> HotTransactionsServices { get; private set; }
        public DbSet<HotTransactionsServicesVw> HotTransactionsServicesVws { get; private set; }
        public DbSet<HotTransactionsStatus> HotTransactionsStatuses { get; private set; }
        public DbSet<HotTransactionsType> HotTransactionsTypes { get; private set; }

        public DbSet<HotTransactionsStatus> HotTransactionsStatuss { get; private set; }
        public DbSet<HotTransactionsVw> HotTransactionsVws { get; private set; }

        public DbSet<HotTypeRoom> HotTypeRooms { get; private set; }

        public DbSet<HotService> HotServices { get; private set; }
        #endregion

        #region ======= WH ========================

        public DbSet<WhUnit> WhUnits { get; private set; }

        #endregion ======= WH ========================

        #region =============== FXA ===========================
        public DbSet<FxaAdditionsExclusion> FxaAdditionsExclusions { get; private set; }
        public DbSet<FxaAdditionsExclusionVw> FxaAdditionsExclusionVws { get; private set; }

        public DbSet<FxaAdditionsExclusionType> FxaAdditionsExclusionTypes { get; private set; }
        public DbSet<FxaDepreciationMethod> FxaDepreciationMethods { get; private set; }

        public DbSet<FxaFixedAsset> FxaFixedAssets { get; private set; }
        public DbSet<FxaFixedAssetVw> FxaFixedAssetVws { get; private set; }
        public DbSet<FxaFixedAssetVw2> FxaFixedAssetVw2s { get; private set; }

        public DbSet<FxaFixedAssetTransfer> FxaFixedAssetTransfers { get; private set; }
        public DbSet<FxaFixedAssetTransferVw> FxaFixedAssetTransferVws { get; private set; }

        public DbSet<FxaFixedAssetType> FxaFixedAssetTypes { get; private set; }
        public DbSet<FxaFixedAssetTypeVw> FxaFixedAssetTypeVws { get; private set; }

        public DbSet<FxaTransaction> FxaTransactions { get; private set; }
        public DbSet<FxaTransactionsVw> FxaTransactionsVws { get; private set; }

        public DbSet<FxaTransactionsAssest> FxaTransactionsAssests { get; private set; }
        public DbSet<FxaTransactionsAssestVw> FxaTransactionsAssestVws { get; private set; }

        public DbSet<FxaTransactionsPayment> FxaTransactionsPayments { get; private set; }

        public DbSet<FxaTransactionsType> FxaTransactionsTypes { get; private set; }

        public DbSet<FxaTransactionsProduct> FxaTransactionsProducts { get; private set; }
        public DbSet<FxaTransactionsProductsVw> FxaTransactionsProductsVws { get; private set; }

        public DbSet<FxaTransactionsRevaluation> FxaTransactionsRevaluations { get; private set; }
        public DbSet<FxaTransactionsRevaluationVw> FxaTransactionsRevaluationVws { get; private set; }
        #endregion

        #region =============== CRM ===========================

        public DbSet<CrmEmailTemplate> CrmEmailTemplates { get; private set; }
        public DbSet<CrmEmailTemplateAttach> CrmEmailTemplateAttachs { get; private set; }

        #endregion

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<TraceEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = session.UserId;
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.IsDeleted = false;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = session.UserId;
                        entry.Entity.ModifiedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.ModifiedBy = session.UserId;
                        entry.Entity.ModifiedOn = DateTime.UtcNow;
                        entry.Entity.IsDeleted = true;
                        break;
                }

            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region ========= Main ======================================================================
            modelBuilder.ApplyConfiguration(new SysCalendarConfig());
            modelBuilder.ApplyConfiguration(new SysSystemConfig());
            modelBuilder.ApplyConfiguration(new SysScreenConfig());
            modelBuilder.ApplyConfiguration(new SysAnnouncementConfig());
            modelBuilder.ApplyConfiguration(new SysAnnouncementVwConfig());
            modelBuilder.ApplyConfiguration(new SysAnnouncementLocationVwConfig());
            modelBuilder.ApplyConfiguration(new SysLookupCategoryConfig());
            modelBuilder.ApplyConfiguration(new SysLookupDataConfig());
            modelBuilder.ApplyConfiguration(new SysLookupDataVwConfig());
            modelBuilder.ApplyConfiguration(new SysLookupCategoryConfig());
            modelBuilder.ApplyConfiguration(new SysDepartmentConfig());
            modelBuilder.ApplyConfiguration(new SysDepartmentVwConfig());
            modelBuilder.ApplyConfiguration(new SysDepartmentCatagoryConfig());
            modelBuilder.ApplyConfiguration(new SysUserConfig());
            modelBuilder.ApplyConfiguration(new SysGroupConfig());
            modelBuilder.ApplyConfiguration(new SysGroupVwConfig());
            modelBuilder.ApplyConfiguration(new SysScreenPermissionConfig());
            modelBuilder.ApplyConfiguration(new SysScreenPermissionVwConfig());
            modelBuilder.ApplyConfiguration(new SysBranchVwConfig());
            modelBuilder.ApplyConfiguration(new SysNotificationConfig());
            modelBuilder.ApplyConfiguration(new SysNotificationsVwConfig());
            modelBuilder.ApplyConfiguration(new SysPropertyConfig());
            modelBuilder.ApplyConfiguration(new SysPropertiesVwConfig());
            modelBuilder.ApplyConfiguration(new SysPropertyValuesVwConfig());
            modelBuilder.ApplyConfiguration(new SysScreenPropertyConfig());
            modelBuilder.ApplyConfiguration(new SysScreenPermissionPropertyConfig());
            modelBuilder.ApplyConfiguration(new SysScreenPermissionPropertiesVwConfig());
            modelBuilder.ApplyConfiguration(new SysCustomerGroupConfig());
            modelBuilder.ApplyConfiguration(new SysCustomerTypeConfig());
            modelBuilder.ApplyConfiguration(new SysCustomerGroupAccountConfig());
            modelBuilder.ApplyConfiguration(new SysCustomerGroupAccountsVwConfig());
            modelBuilder.ApplyConfiguration(new SysLicenseConfig());
            modelBuilder.ApplyConfiguration(new SysLicenseVwConfig());

            modelBuilder.ApplyConfiguration(new SysFavMenuConfig());
            modelBuilder.ApplyConfiguration(new SysFileConfig());

            modelBuilder.ApplyConfiguration(new InvestBranchConfig());
            modelBuilder.ApplyConfiguration(new InvestBranchVwConfig());

            modelBuilder.ApplyConfiguration(new SysCustomerConfig());
            modelBuilder.ApplyConfiguration(new SysCurrencyConfig());
            modelBuilder.ApplyConfiguration(new SysCurrencyListVwConfig());

            modelBuilder.ApplyConfiguration(new SysExchangeRateConfig());
            modelBuilder.ApplyConfiguration(new SysExchangeRateVwConfig());
            modelBuilder.ApplyConfiguration(new SysExchangeRateListVWConfig());

            modelBuilder.ApplyConfiguration(new SysScreenInstalledConfig());
            modelBuilder.ApplyConfiguration(new SysScreenInstalledVwConfig());
            modelBuilder.ApplyConfiguration(new SysTasksVwConfig());
            modelBuilder.ApplyConfiguration(new SysCustomerBranchConfig());
            modelBuilder.ApplyConfiguration(new SysCustomerBranchVwConfig());
            modelBuilder.ApplyConfiguration(new SysCitesConfig());
            modelBuilder.ApplyConfiguration(new SysCustomerCoTypeConfig());
            modelBuilder.ApplyConfiguration(new SysUserVwConfig());
            modelBuilder.ApplyConfiguration(new SysVatGroupConfig());

            modelBuilder.ApplyConfiguration(new SysPoliciesProcedureConfig());
            modelBuilder.ApplyConfiguration(new SysPoliciesProceduresVwConfig());


            modelBuilder.ApplyConfiguration(new SysCustomerVwConfig());
            modelBuilder.ApplyConfiguration(new SysCustomerContactConfig());
            modelBuilder.ApplyConfiguration(new SysCustomerContactVwConfig());
            modelBuilder.ApplyConfiguration(new SysCustomerFileConfig());
            modelBuilder.ApplyConfiguration(new SysCustomerFilesVwConfig());

            modelBuilder.ApplyConfiguration(new SysTemplateConfig());
            modelBuilder.ApplyConfiguration(new SysTemplateVwConfig());

            modelBuilder.ApplyConfiguration(new SysFormConfig());
            modelBuilder.ApplyConfiguration(new SysFormVwConfig());
            modelBuilder.ApplyConfiguration(new SysFormVwConfig());

            modelBuilder.ApplyConfiguration(new SysSettingExportConfig());
            modelBuilder.ApplyConfiguration(new SysSettingExportVwConfig());

            modelBuilder.ApplyConfiguration(new SysActivityLogConfig());
            modelBuilder.ApplyConfiguration(new SysActivityLogVwConfig());

            modelBuilder.ApplyConfiguration(new InvestEmployeeConfig());
            modelBuilder.ApplyConfiguration(new InvestEmployeeVvwConfig());

            modelBuilder.ApplyConfiguration(new SysUserLogTimeConfig());
            modelBuilder.ApplyConfiguration(new SysUserLogTimeVwConfig());

            modelBuilder.ApplyConfiguration(new SysUserTrackingConfig());
            modelBuilder.ApplyConfiguration(new SysUserTrackingVwConfig());

            modelBuilder.ApplyConfiguration(new SysUserTypeConfig());
            modelBuilder.ApplyConfiguration(new SysUserType2Config());

            modelBuilder.ApplyConfiguration(new SysDynamicAttributeConfig());
            modelBuilder.ApplyConfiguration(new SysDynamicAttributesVwConfig());
            modelBuilder.ApplyConfiguration(new SysDynamicAttributeDataTypeConfig());
            modelBuilder.ApplyConfiguration(new SysScreenVwConfig());
            modelBuilder.ApplyConfiguration(new MonthDayConfig());
            modelBuilder.ApplyConfiguration(new SysVatGroupVwConfig());

            modelBuilder.ApplyConfiguration(new SysNotificationsSettingConfig());
            modelBuilder.ApplyConfiguration(new SysNotificationsSettingVwConfig());
            modelBuilder.ApplyConfiguration(new SysMailServerConfig());
            modelBuilder.ApplyConfiguration(new SysCountryConfig());
            modelBuilder.ApplyConfiguration(new SysCountryVwConfig());

            modelBuilder.ApplyConfiguration(new SysNotificationsMangConfig());
            modelBuilder.ApplyConfiguration(new SysNotificationsMangVwConfig());

            modelBuilder.ApplyConfiguration(new SysTableConfig());
            modelBuilder.ApplyConfiguration(new SysTableFieldConfig());

            modelBuilder.ApplyConfiguration(new SysPackagesPropertyValueConfig());

            modelBuilder.ApplyConfiguration(new SysZatcaInvoiceTypeConfig());

            #endregion ------- End Main -------------------------------------------------------------------

            #region ========= HR ======================================================================
            modelBuilder.ApplyConfiguration(new HrAttLocationConfig());
            modelBuilder.ApplyConfiguration(new HrEmployeeConfig());
            modelBuilder.ApplyConfiguration(new HrEmployeeVwConfig());

            modelBuilder.ApplyConfiguration(new HrEvaluationAnnualIncreaseConfigConfig());

            modelBuilder.ApplyConfiguration(new HrNotificationConfig());
            modelBuilder.ApplyConfiguration(new HrNotificationsVwConfig());

            modelBuilder.ApplyConfiguration(new HrCompetenceConfig());
            modelBuilder.ApplyConfiguration(new HrCompetencesVwConfig());

            modelBuilder.ApplyConfiguration(new HrCompetencesCatagoryConfig());

            modelBuilder.ApplyConfiguration(new HrTrainingBagConfig());
            modelBuilder.ApplyConfiguration(new HrTrainingBagVwConfig());

            modelBuilder.ApplyConfiguration(new HrPolicyConfig());
            modelBuilder.ApplyConfiguration(new HrPoliciesVwConfig());

            modelBuilder.ApplyConfiguration(new HrPoliciesTypeConfig());

            modelBuilder.ApplyConfiguration(new HrKpiTemplatesVwConfig());
            modelBuilder.ApplyConfiguration(new HrKpiTemplateConfig());
            modelBuilder.ApplyConfiguration(new HrKpiTemplatesCompetenceConfig());
            modelBuilder.ApplyConfiguration(new HrSettingConfig());

            modelBuilder.ApplyConfiguration(new HrSalaryGroupConfig());
            modelBuilder.ApplyConfiguration(new HrSalaryGroupVwConfig());

            modelBuilder.ApplyConfiguration(new HrVacationsTypeConfig());
            modelBuilder.ApplyConfiguration(new HrVacationsTypeVwConfig());

            modelBuilder.ApplyConfiguration(new HrDisciplinaryCaseConfig());
            modelBuilder.ApplyConfiguration(new HrCardTemplateConfig());
            modelBuilder.ApplyConfiguration(new hrOverTimeDVwConfig());
            modelBuilder.ApplyConfiguration(new HrPayrollTypeConfig());
            modelBuilder.ApplyConfiguration(new HrAttShiftConfig());
            modelBuilder.ApplyConfiguration(new HrJobVwConfig());
            modelBuilder.ApplyConfiguration(new HrJobProgramVwConfig());
            modelBuilder.ApplyConfiguration(new HrAbsenceConfig());

            modelBuilder.ApplyConfiguration(new HrVacationsConfig());
            modelBuilder.ApplyConfiguration(new HrVacationsVwConfig());
            modelBuilder.ApplyConfiguration(new HrMandateConfig());
            modelBuilder.ApplyConfiguration(new HrMandateVwConfig());
            modelBuilder.ApplyConfiguration(new HrAttendanceConfig());
            modelBuilder.ApplyConfiguration(new HrAttendancesVwConfig());
            modelBuilder.ApplyConfiguration(new HrAttTimeTableConfig());
            modelBuilder.ApplyConfiguration(new HrAttTimeTableVwConfig());
            modelBuilder.ApplyConfiguration(new HrAttShiftEmployeeConfig());
            modelBuilder.ApplyConfiguration(new HrAttShiftEmployeeVwConfig());
            modelBuilder.ApplyConfiguration(new HrKpiTypeConfig());
            modelBuilder.ApplyConfiguration(new HrKpiTemplatesCompetencesVwConfig());
            modelBuilder.ApplyConfiguration(new HrDisciplinaryRuleConfig());
            modelBuilder.ApplyConfiguration(new HrDisciplinaryRuleVwConfig());
            modelBuilder.ApplyConfiguration(new HrDisciplinaryCaseActionConfig());
            modelBuilder.ApplyConfiguration(new HrDisciplinaryCaseActionVwConfig());
            modelBuilder.ApplyConfiguration(new HrRateTypeConfig());
            modelBuilder.ApplyConfiguration(new HrRateTypeVwConfig());
            modelBuilder.ApplyConfiguration(new HrVacationsCatagoryConfig());
            modelBuilder.ApplyConfiguration(new HrAllowanceDeductionConfig());
            modelBuilder.ApplyConfiguration(new HrAllowanceDeductionVWConfig());
            modelBuilder.ApplyConfiguration(new HrLoanConfig());
            modelBuilder.ApplyConfiguration(new HrLoanVwConfig());
            modelBuilder.ApplyConfiguration(new HrAbsenceVwConfig());
            modelBuilder.ApplyConfiguration(new HrPayrollDVwConfig());
            modelBuilder.ApplyConfiguration(new HrPayrollDConfig());
            //       2023/12/31
            modelBuilder.ApplyConfiguration(new HrArchivesFileConfig());
            modelBuilder.ApplyConfiguration(new HrArchivesFilesVwConfig());

            modelBuilder.ApplyConfiguration(new HrLicenseConfig());
            modelBuilder.ApplyConfiguration(new HrLicensesVwConfig());

            modelBuilder.ApplyConfiguration(new HrTransferConfig());
            modelBuilder.ApplyConfiguration(new HrTransfersVwConfig());

            modelBuilder.ApplyConfiguration(new HrOverTimeMConfig());
            modelBuilder.ApplyConfiguration(new HrOverTimeMVwConfig());

            modelBuilder.ApplyConfiguration(new HrOhadDetailConfig());
            modelBuilder.ApplyConfiguration(new HrOhadDetailsVwConfig());

            modelBuilder.ApplyConfiguration(new HrEmpWarnConfig());
            modelBuilder.ApplyConfiguration(new HrEmpWarnVwConfig());

            modelBuilder.ApplyConfiguration(new HrVacationBalanceConfig());
            modelBuilder.ApplyConfiguration(new HrVacationBalanceVwConfig());

            modelBuilder.ApplyConfiguration(new HrDependentConfig());
            modelBuilder.ApplyConfiguration(new HrDependentsVwConfig());

            modelBuilder.ApplyConfiguration(new HrDirectJobConfig());
            modelBuilder.ApplyConfiguration(new HrDirectJobVwConfig());

            modelBuilder.ApplyConfiguration(new HrIncrementConfig());
            modelBuilder.ApplyConfiguration(new HrIncrementsVwConfig());

            modelBuilder.ApplyConfiguration(new HrLeaveConfig());
            modelBuilder.ApplyConfiguration(new HrLeaveVwConfig());
            modelBuilder.ApplyConfiguration(new HrKpiConfig());
            modelBuilder.ApplyConfiguration(new HrKpiVwConfig());
            modelBuilder.ApplyConfiguration(new HrKpiDetaileConfig());
            modelBuilder.ApplyConfiguration(new HrKpiDetailesVwConfig());
            modelBuilder.ApplyConfiguration(new HrEmpWorkTimeConfig());
            modelBuilder.ApplyConfiguration(new HrEmpWorkTimeVwConfig());
            modelBuilder.ApplyConfiguration(new HrSalaryGroupAccountConfig());
            modelBuilder.ApplyConfiguration(new HrSalaryGroupRefranceConfig());
            modelBuilder.ApplyConfiguration(new HrSalaryGroupRefranceVwConfig());
            modelBuilder.ApplyConfiguration(new HrSalaryGroupAllowanceVwConfig());
            modelBuilder.ApplyConfiguration(new HrSalaryGroupDeductionVwConfig());
            modelBuilder.ApplyConfiguration(new HrNotificationsTypeVwConfig());
            modelBuilder.ApplyConfiguration(new HrNotificationsTypeConfig());
            modelBuilder.ApplyConfiguration(new HrNotificationsSettingConfig());
            modelBuilder.ApplyConfiguration(new HrNotificationsSettingVwConfig());
            modelBuilder.ApplyConfiguration(new HrAttTimeTableDayConfig());
            modelBuilder.ApplyConfiguration(new HrDeductionVwConfig());
            modelBuilder.ApplyConfiguration(new HrAllowanceVwConfig());

            modelBuilder.ApplyConfiguration(new HrAllowanceDeductionMConfig());

            modelBuilder.ApplyConfiguration(new HrAllowanceDeductionTempOrFixConfig());

            modelBuilder.ApplyConfiguration(new HrArchiveFilesDetailsConfig());
            modelBuilder.ApplyConfiguration(new HrArchiveFilesDetailsVwConfig());

            modelBuilder.ApplyConfiguration(new HrAttActionConfig());
            modelBuilder.ApplyConfiguration(new HrAssignmenConfig());
            modelBuilder.ApplyConfiguration(new HrAssignmenVwConfig());

            modelBuilder.ApplyConfiguration(new HrAttLocationEmployeeConfig());
            modelBuilder.ApplyConfiguration(new HrAttLocationEmployeeVwConfig());

            modelBuilder.ApplyConfiguration(new HrAttShiftCloseConfig());
            modelBuilder.ApplyConfiguration(new HrAttShiftCloseVwConfig());

            modelBuilder.ApplyConfiguration(new HrAttShiftCloseDConfig());

            modelBuilder.ApplyConfiguration(new HrAuthorizationConfig());
            modelBuilder.ApplyConfiguration(new HrAuthorizationVwConfig());
            modelBuilder.ApplyConfiguration(new HrAttendanceBioTimeConfig());

            modelBuilder.ApplyConfiguration(new HrCheckInOutConfig());
            modelBuilder.ApplyConfiguration(new HrCheckInOutVwConfig());
            modelBuilder.ApplyConfiguration(new HrClearanceConfig());
            modelBuilder.ApplyConfiguration(new HrClearanceVwConfig());
            modelBuilder.ApplyConfiguration(new HrClearanceTypeConfig());
            modelBuilder.ApplyConfiguration(new HrClearanceTypeVwConfig());

            modelBuilder.ApplyConfiguration(new HrCompensatoryVacationConfig());
            modelBuilder.ApplyConfiguration(new HrCompensatoryVacationsVwConfig());


            modelBuilder.ApplyConfiguration(new HrContracteConfig());
            modelBuilder.ApplyConfiguration(new HrContractesVwConfig());

            modelBuilder.ApplyConfiguration(new HrClearanceMonthConfig());
            modelBuilder.ApplyConfiguration(new HrCostTypeConfig());
            modelBuilder.ApplyConfiguration(new HrCostTypeVwConfig());

            modelBuilder.ApplyConfiguration(new HrCustodyConfig());
            modelBuilder.ApplyConfiguration(new HrCustodyVwConfig());
            modelBuilder.ApplyConfiguration(new HrCustodyItemConfig());
            modelBuilder.ApplyConfiguration(new HrCustodyItemsVwConfig());
            modelBuilder.ApplyConfiguration(new HrCustodyItemsPropertyConfig());
            modelBuilder.ApplyConfiguration(new HrCustodyRefranceTypeConfig());
            modelBuilder.ApplyConfiguration(new HrCustodyTypeConfig());
            modelBuilder.ApplyConfiguration(new HrDecisionConfig());
            modelBuilder.ApplyConfiguration(new HrDecisionsVwConfig());
            modelBuilder.ApplyConfiguration(new HrDecisionsEmployeeConfig());
            modelBuilder.ApplyConfiguration(new HrDecisionsEmployeeVwConfig());
            modelBuilder.ApplyConfiguration(new HrOhadConfig());
            modelBuilder.ApplyConfiguration(new HrOhadVwConfig());
            modelBuilder.ApplyConfiguration(new HrNoteConfig());
            modelBuilder.ApplyConfiguration(new HrNoteVwConfig());
            modelBuilder.ApplyConfiguration(new HrIncrementsAllowanceDeductionConfig());
            modelBuilder.ApplyConfiguration(new HrDelayVwConfig());
            modelBuilder.ApplyConfiguration(new HrHolidayConfig());
            modelBuilder.ApplyConfiguration(new HrHolidayVwConfig());
            modelBuilder.ApplyConfiguration(new HrPermissionConfig());
            modelBuilder.ApplyConfiguration(new HrPermissionsVwConfig());


            modelBuilder.ApplyConfiguration(new HrAttShiftTimeTableConfig());
            modelBuilder.ApplyConfiguration(new HrAttShiftTimeTableVwConfig());
            modelBuilder.ApplyConfiguration(new HrEmployeeCostConfig());
            modelBuilder.ApplyConfiguration(new HrEmployeeCostVwConfig());

            modelBuilder.ApplyConfiguration(new HrInsurancePolicyConfig());
            modelBuilder.ApplyConfiguration(new HrInsuranceConfig());
            modelBuilder.ApplyConfiguration(new HrInsuranceEmpConfig());
            modelBuilder.ApplyConfiguration(new HrInsuranceEmpVwConfig());

            modelBuilder.ApplyConfiguration(new HrJobEmployeeVwConfig());
            modelBuilder.ApplyConfiguration(new HrJobDescriptionConfig());
            modelBuilder.ApplyConfiguration(new HrJobConfig());
            modelBuilder.ApplyConfiguration(new HrJobLevelConfig());


            modelBuilder.ApplyConfiguration(new HrRecruitmentApplicationVwConfig());
            modelBuilder.ApplyConfiguration(new HrRecruitmentApplicationConfig());
            modelBuilder.ApplyConfiguration(new HrRecruitmentVacancyConfig());
            modelBuilder.ApplyConfiguration(new HrRecruitmentVacancyVwConfig());
            modelBuilder.ApplyConfiguration(new HrRecruitmentCandidateConfig());
            modelBuilder.ApplyConfiguration(new HrRecruitmentCandidateVwConfig());




            modelBuilder.ApplyConfiguration(new HrVacancyStatusVwConfig());
            modelBuilder.ApplyConfiguration(new HrJobGradeConfig());
            modelBuilder.ApplyConfiguration(new HrJobGradeVwConfig());


            modelBuilder.ApplyConfiguration(new HrRecruitmentCandidateKpiDConfig());
            modelBuilder.ApplyConfiguration(new HrRecruitmentCandidateKpiDVwConfig());
            modelBuilder.ApplyConfiguration(new HrRecruitmentCandidateKpiConfig());
            modelBuilder.ApplyConfiguration(new HrRecruitmentCandidateKpiVwConfig());
            modelBuilder.ApplyConfiguration(new HrPayrollConfig());
            modelBuilder.ApplyConfiguration(new HrPayrollVwConfig());
            modelBuilder.ApplyConfiguration(new HrTicketConfig());
            modelBuilder.ApplyConfiguration(new HrTicketVwConfig());
            modelBuilder.ApplyConfiguration(new HrVisaVwConfig());
            modelBuilder.ApplyConfiguration(new HrVisaConfig());
            modelBuilder.ApplyConfiguration(new HrFixingEmployeeSalaryConfig());
            modelBuilder.ApplyConfiguration(new HrFixingEmployeeSalaryVwConfig());
            modelBuilder.ApplyConfiguration(new HrLeaveTypeConfig());
            modelBuilder.ApplyConfiguration(new HrLeaveTypeVwConfig());
            modelBuilder.ApplyConfiguration(new HrPayrollAllowanceDeductionConfig());
            modelBuilder.ApplyConfiguration(new HrPayrollAllowanceDeductionVwConfig());
            modelBuilder.ApplyConfiguration(new HrLoanInstallmentPaymentConfig());
            modelBuilder.ApplyConfiguration(new HrLoanInstallmentPaymentVwConfig());
            modelBuilder.ApplyConfiguration(new HrLoanInstallmentConfig());
            modelBuilder.ApplyConfiguration(new HrLoanInstallmentVwConfig());
            modelBuilder.ApplyConfiguration(new HrPayrollNoteConfig());
            modelBuilder.ApplyConfiguration(new HrPayrollNoteVwConfig());
            modelBuilder.ApplyConfiguration(new HrDecisionsTypeConfig());
            modelBuilder.ApplyConfiguration(new HrDecisionsTypeVwConfig());
            modelBuilder.ApplyConfiguration(new HrDecisionsTypeEmployeeConfig());
            modelBuilder.ApplyConfiguration(new HrDecisionsTypeEmployeeVwConfig());





            #endregion ------- End HR -------------------------------------------------------------------

            #region ========= ACC ======================================================================
            modelBuilder.ApplyConfiguration(new AccAccountConfig());
            modelBuilder.ApplyConfiguration(new AccFacilityConfig());
            modelBuilder.ApplyConfiguration(new AccFacilitiesVwConfig());
            modelBuilder.ApplyConfiguration(new AccFinancialYearConfig());
            modelBuilder.ApplyConfiguration(new AccGroupConfig());
            modelBuilder.ApplyConfiguration(new AccPeriodsConfig());
            modelBuilder.ApplyConfiguration(new AccCostCenterConfig());
            modelBuilder.ApplyConfiguration(new AccCostCenterVwsConfig());
            modelBuilder.ApplyConfiguration(new AccJournalMasterConfig());
            modelBuilder.ApplyConfiguration(new AccJournalMasterVwConfig());
            //modelBuilder.ApplyConfiguration(new AccCostCenterVwsConfig());
            modelBuilder.ApplyConfiguration(new WhItemsVwConfig());
            modelBuilder.ApplyConfiguration(new AccReferenceTypeConfig());
            modelBuilder.ApplyConfiguration(new AccAccountsVwConfig());
            modelBuilder.ApplyConfiguration(new AccBranchAccountConfig());
            modelBuilder.ApplyConfiguration(new AccBranchAccountsVwConfig());
            modelBuilder.ApplyConfiguration(new AccBranchAccountTypeConfig());
            modelBuilder.ApplyConfiguration(new AccPeriodDateVwsConfig());
            modelBuilder.ApplyConfiguration(new AccJournalDetaileConfig());
            modelBuilder.ApplyConfiguration(new AccRequestConfig());
            modelBuilder.ApplyConfiguration(new AccRequestVwConfig());
            modelBuilder.ApplyConfiguration(new AccJournalDetailesVwConfig());
            modelBuilder.ApplyConfiguration(new AccCashonhandListVWConfig());
            modelBuilder.ApplyConfiguration(new AccAccountsSubHelpeVwConfig());
            modelBuilder.ApplyConfiguration(new AccCostCenteHelpVwConfig());
            modelBuilder.ApplyConfiguration(new AccJournalSignatureVwConfig());
            modelBuilder.ApplyConfiguration(new AccJournalMasterExportVwConfig());
            modelBuilder.ApplyConfiguration(new AccBankVwConfig());
            modelBuilder.ApplyConfiguration(new AccBanksConfig());
            modelBuilder.ApplyConfiguration(new AccAccountsLevelConfig());
            modelBuilder.ApplyConfiguration(new AccPettyCashExpensesTypeConfig());
            modelBuilder.ApplyConfiguration(new AccPettyCashExpensesTypeVwConfig());
            modelBuilder.ApplyConfiguration(new AccBankChequeBookConfig());
            modelBuilder.ApplyConfiguration(new AccCashOnHandVwConfig());
            modelBuilder.ApplyConfiguration(new AccAccountsGroupsFinalVwConfig());
            modelBuilder.ApplyConfiguration(new AccAccountsCostcenterVwConfig());
            modelBuilder.ApplyConfiguration(new AccDocumentTypeListVwConfig());
            modelBuilder.ApplyConfiguration(new AccReferenceTypeVwConfig());
            #endregion ------- End ACC -------------------------------------------------------------------

            #region ======= PM ========================
            modelBuilder.ApplyConfiguration(new PmProjectsTypeConfig());
            modelBuilder.ApplyConfiguration(new PmJobsSalaryConfig());
            modelBuilder.ApplyConfiguration(new PmJobsSalaryVwConfig());
            modelBuilder.ApplyConfiguration(new PmOperationalControlConfig());
            modelBuilder.ApplyConfiguration(new PmOperationalControlsVwConfig());

            modelBuilder.ApplyConfiguration(new PmExtractAdditionalTypeConfig());
            modelBuilder.ApplyConfiguration(new PmExtractAdditionalTypeVwConfig());
            modelBuilder.ApplyConfiguration(new PmProjectPlanConfig());
            modelBuilder.ApplyConfiguration(new PmProjectPlansVwConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsAddDeducConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsAddDeducVwConfig());
            modelBuilder.ApplyConfiguration(new PMProjectsConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsFileConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsFilesVwConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsInstallmentActionConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsInstallmentActionVwConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsInstallmentConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsInstallmentPaymentConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsInstallmentPaymentVwConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsInstallmentVwConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsItemConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsItemsVwConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsRiskConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsRisksVwConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsRisksVw2Config());
            modelBuilder.ApplyConfiguration(new PmProjectsStaffConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsStaffTypeConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsStaffVwConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsStageConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsStagesVwConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsStokeholderConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsStokeholderVwConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsTypeVwConfig());
            modelBuilder.ApplyConfiguration(new PmProjectsVwConfig());
            modelBuilder.ApplyConfiguration(new PmProjectStatusVwConfig());
            modelBuilder.ApplyConfiguration(new HrDisciplinaryActionTypeConfig());

            #endregion ======= PM ========================

            #region ========= RPT ======================================================================
            modelBuilder.ApplyConfiguration(new RptReportConfig());
            modelBuilder.ApplyConfiguration(new RptCustomReportConfig());
            #endregion ------- End RPT -------------------------------------------------------------------

            #region ========= WhatsApp ======================================================================
            modelBuilder.ApplyConfiguration(new WaWhatsappSettingConfig());
            modelBuilder.ApplyConfiguration(new WaTemplateMessageValueConfig());
            modelBuilder.ApplyConfiguration(new WaTemplateMessageConfig());
            modelBuilder.ApplyConfiguration(new WaDirectMessageConfig());


            #endregion ------- End WhatsApp -------------------------------------------------------------------

            #region //=============================Gb======================
            modelBuilder.ApplyConfiguration(new BudgTransactionConfig());
            modelBuilder.ApplyConfiguration(new BudgTransactionVwConfig());
            modelBuilder.ApplyConfiguration(new BudgTransactionDetaileConfig());
            modelBuilder.ApplyConfiguration(new BudgTransactionDetailesVwConfig());
            modelBuilder.ApplyConfiguration(new BudgExpensesLinksConfig());
            modelBuilder.ApplyConfiguration(new BudgExpensesLinksVWConfig());
            modelBuilder.ApplyConfiguration(new BudgDocTypeConfig());
            modelBuilder.ApplyConfiguration(new BudgAccountExpensesConfig());
            modelBuilder.ApplyConfiguration(new BudgAccountExpensesVWConfig());
            modelBuilder.ApplyConfiguration(new BudgTransactionBalanceVWConfig());

            #endregion==================End GB====================
            // OnModelCreatingPartial(modelBuilder);

            #region =============SAL===================================================================
            modelBuilder.ApplyConfiguration(new SalTransactionConfig());
            modelBuilder.ApplyConfiguration(new SalTransactionVwConfig());

            modelBuilder.ApplyConfiguration(new SalItemsPriceMConfig());
            modelBuilder.ApplyConfiguration(new SalItemsPriceMVwConfig());

            modelBuilder.ApplyConfiguration(new SalPosSettingConfig());
            modelBuilder.ApplyConfiguration(new SalPosSettingVwConfig());

            modelBuilder.ApplyConfiguration(new SalPosUserConfig());
            modelBuilder.ApplyConfiguration(new SalPosUsersVwConfig());
            modelBuilder.ApplyConfiguration(new SalTransactionsTypeConfig());

            modelBuilder.ApplyConfiguration(new SalTransactionsCommissionConfig());
            modelBuilder.ApplyConfiguration(new SalTransactionsCommissionVwConfig());


            #endregion =========End SAL===============================================================

            #region =============OPM==================================================================
            modelBuilder.ApplyConfiguration(new OpmContractConfig());
            modelBuilder.ApplyConfiguration(new OpmContractVwConfig());
            modelBuilder.ApplyConfiguration(new OpmContractLocationConfig());
            modelBuilder.ApplyConfiguration(new OpmTransactionsItemConfig());
            modelBuilder.ApplyConfiguration(new OpmTransactionsLocationConfig());
            modelBuilder.ApplyConfiguration(new OpmPolicyConfig());

            modelBuilder.ApplyConfiguration(new OpmContractItemConfig());
            modelBuilder.ApplyConfiguration(new OpmContarctEmpConfig());
            modelBuilder.ApplyConfiguration(new OpmContarctAssignConfig());
            modelBuilder.ApplyConfiguration(new OpmContractVwConfig());

            modelBuilder.ApplyConfiguration(new OpmContractReplaceEmpConfig());
            modelBuilder.ApplyConfiguration(new OpmContarctEmpVwConfig());

            modelBuilder.ApplyConfiguration(new OPMPayrollConfig());
            modelBuilder.ApplyConfiguration(new OpmPayrollVwConfig());

            modelBuilder.ApplyConfiguration(new OpmContractReplaceEmpVwConfig());
            modelBuilder.ApplyConfiguration(new OpmContractItemsVwConfig());
            modelBuilder.ApplyConfiguration(new OPMPayrollDVWConfig());
            modelBuilder.ApplyConfiguration(new OPMContractLocationVWConfig());
            modelBuilder.ApplyConfiguration(new OpmServicesTypesVWConfig());
            modelBuilder.ApplyConfiguration(new OPMTransactionsDetailsVwConfig());
            modelBuilder.ApplyConfiguration(new OpmPurtransactionsDetailsVwConfig());

            modelBuilder.ApplyConfiguration(new OpmContarctEmpPopUpVwConfig());
            #endregion==================End OPM====================

            #region =============PUR=====================================
            modelBuilder.ApplyConfiguration(new PURTransactionsConfig());
            modelBuilder.ApplyConfiguration(new PURTransactionsVwsConfig());

            #endregion

            #region ==============WF========================================================
            modelBuilder.ApplyConfiguration(new WfAppGroupConfig());
            modelBuilder.ApplyConfiguration(new WfAppGroupsVwConfig());

            modelBuilder.ApplyConfiguration(new WfAppTypeConfig());
            modelBuilder.ApplyConfiguration(new WfAppTypeVwConfig());

            modelBuilder.ApplyConfiguration(new WfAppTypeTableConfig());
            modelBuilder.ApplyConfiguration(new WfLookUpCatagoryConfig());
            modelBuilder.ApplyConfiguration(new WfStepsTransactionsVwConfig());
            modelBuilder.ApplyConfiguration(new WfStepsTransactionConfig());
            modelBuilder.ApplyConfiguration(new WfStepConfig());
            modelBuilder.ApplyConfiguration(new WfStepsVwConfig());
            modelBuilder.ApplyConfiguration(new WfApplicationsStatusConfig());
            modelBuilder.ApplyConfiguration(new WfApplicationsStatusConfig());
            modelBuilder.ApplyConfiguration(new WfApplicationConfig());
            modelBuilder.ApplyConfiguration(new WfApplicationsVwConfig());
            modelBuilder.ApplyConfiguration(new WfDynamicValueConfig());
            modelBuilder.ApplyConfiguration(new WfStatusConfig());
            modelBuilder.ApplyConfiguration(new WfStepsNotificationConfig());
            #endregion

            #region ======== HOT============
            modelBuilder.ApplyConfiguration(new EntityConfigurations.HOT.HotFloorConfig());
            modelBuilder.ApplyConfiguration(new HotFloorsVwConfig());
            modelBuilder.ApplyConfiguration(new HotGroupConfig());
            modelBuilder.ApplyConfiguration(new HotRoomAssetConfig());
            modelBuilder.ApplyConfiguration(new HotRoomAssetsVwConfig());
            modelBuilder.ApplyConfiguration(new HotRoomConfig());
            modelBuilder.ApplyConfiguration(new HotRoomServiceConfig());
            modelBuilder.ApplyConfiguration(new HotServiceConfig());
            modelBuilder.ApplyConfiguration(new HotServicesVwConfig());
            modelBuilder.ApplyConfiguration(new HotTransactionConfig());
            modelBuilder.ApplyConfiguration(new HotTransactionsCompanionConfig());
            modelBuilder.ApplyConfiguration(new HotTransactionsCompanionVwConfig());
            modelBuilder.ApplyConfiguration(new HotTransactionsPaymentConfig());
            modelBuilder.ApplyConfiguration(new HotTransactionsRoomConfig());
            modelBuilder.ApplyConfiguration(new HotTransactionsRoomVwConfig());
            modelBuilder.ApplyConfiguration(new HotTransactionsServiceConfig());
            modelBuilder.ApplyConfiguration(new HotTransactionsServicesVwConfig());
            modelBuilder.ApplyConfiguration(new HotTransactionsStatusConfig());
            modelBuilder.ApplyConfiguration(new HotTransactionsTypeConfig());
            modelBuilder.ApplyConfiguration(new HotTransactionsVwConfig());



            #endregion

            #region ======= WH ========================
            modelBuilder.ApplyConfiguration(new WhUnitConfig());


            #endregion ======= WH ========================

            #region =============== FXA ===========================
            modelBuilder.ApplyConfiguration(new FxaAdditionsExclusionConfig());
            modelBuilder.ApplyConfiguration(new FxaAdditionsExclusionVwConfig());

            modelBuilder.ApplyConfiguration(new FxaAdditionsExclusionTypeConfig());
            modelBuilder.ApplyConfiguration(new FxaDepreciationMethodConfig());

            modelBuilder.ApplyConfiguration(new FxaFixedAssetConfig());
            modelBuilder.ApplyConfiguration(new FxaFixedAssetVwConfig());
            modelBuilder.ApplyConfiguration(new FxaFixedAssetVw2Config());

            modelBuilder.ApplyConfiguration(new FxaFixedAssetTransferConfig());
            modelBuilder.ApplyConfiguration(new FxaFixedAssetTransferVwConfig());

            modelBuilder.ApplyConfiguration(new FxaFixedAssetTypeConfig());
            modelBuilder.ApplyConfiguration(new FxaFixedAssetTypeVwConfig());

            modelBuilder.ApplyConfiguration(new FxaTransactionConfig());
            modelBuilder.ApplyConfiguration(new FxaTransactionsVwConfig());

            modelBuilder.ApplyConfiguration(new FxaTransactionsAssestConfig());
            modelBuilder.ApplyConfiguration(new FxaTransactionsAssestVwConfig());

            modelBuilder.ApplyConfiguration(new FxaTransactionsPaymentConfig());

            modelBuilder.ApplyConfiguration(new FxaTransactionsTypeConfig());

            modelBuilder.ApplyConfiguration(new FxaTransactionsProductConfig());
            modelBuilder.ApplyConfiguration(new FxaTransactionsProductsVwConfig());

            modelBuilder.ApplyConfiguration(new FxaTransactionsRevaluationConfig());
            modelBuilder.ApplyConfiguration(new FxaTransactionsRevaluationVwConfig());
            #endregion

            #region =============== CRM ===========================

            modelBuilder.ApplyConfiguration(new CrmEmailTemplateAttachConfig());
            modelBuilder.ApplyConfiguration(new CrmEmailTemplateConfig());

            #endregion
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }
        //   partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
