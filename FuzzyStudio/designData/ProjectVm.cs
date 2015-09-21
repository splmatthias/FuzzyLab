using fuzzyStudio.viewModels;

namespace fuzzyStudio.designData
{
    public class ProjectVm : ProjectViewModel
    {
        public ProjectVm() : base("Test Object")
        {
            CurrentView = new ControllerConfigVm();
        }
    }
}
