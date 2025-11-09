namespace Logix.MVC.ViewModels
{
    public class BranchesVM
    {
        //using this VM for Grant Permissions with checkBoxes
        public int BranchId { get; set; }
        public string? BraName { get; set; }
        public bool IsSelected { get; set; }
    }
}
