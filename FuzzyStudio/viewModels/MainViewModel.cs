using System.IO;
using System.Windows.Input;
using fuzzyController.io;
using fuzzyStudio.views;
using Microsoft.Win32;

namespace fuzzyStudio.viewModels
{
    public class MainViewModel : ViewModel<IMainView>
    {
        public MainViewModel(IMainView view)
            : base(view)
        {
            createCommands();

            CreateNewProject.Execute(null);
        }

        public ProjectViewModel Project
        {
            get { return _project; }
            set
            {
                if (SetProperty(ref _project, value, "Project"))
                {
                    RaisePropertyChanged("Title");
                }
            }
        }

        public string Title
        {
            get
            {
                var title = "Fuzzy Studio";
                if (_project != null && !string.IsNullOrEmpty(_project.Name))
                {
                    title += " - " + _project.Name;
                }
                return title;
            }
        }
        
        public ICommand CreateNewProject { get; private set; }

        public ICommand OpenProject { get; private set; }

        public ICommand SaveProject { get; private set; }

        public ICommand SaveProjectAs { get; private set; }

        public ICommand Close { get; private set; }

        public ICommand OpenPlugin { get; private set; }

        private void createCommands()
        {
            CreateNewProject = new DelegateCommand(() => Project = new ProjectViewModel("Unnamed"));
            
            Close = new DelegateCommand(p =>
            {
                if (Project != null && Project.HasChanged)
                {
                    
                }
                View.Close();
            });
            
            OpenPlugin = new DelegateCommand(p =>
            {
                _pluginModule = new RoboSim.MainWindow();
                _pluginModule.Show();
            });

            OpenProject = new DelegateCommand(p =>
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Fuzzy Project File (*.fpf;*.json)|*.fpf;*.json|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    var reader = new ConfigurationIO();
                    var configuration = reader.ReadFromFile(openFileDialog.FileName);
                    Project = new ProjectViewModel(new FileInfo(openFileDialog.FileName).Name, configuration);
                }
            });

            SaveProjectAs = new DelegateCommand(p =>
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Fuzzy Project File (*.fpf;*.json)|*.fpf;*.json|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == true)
                {
                    var reader = new ConfigurationIO();
                    var fuzzyConfiguration = Project.GetFuzzyConfiguration();
                    reader.WriteToFile(fuzzyConfiguration, saveFileDialog.FileName);
                }
            });
        }

        private ProjectViewModel _project;
        private RoboSim.MainWindow _pluginModule;
    }
}
