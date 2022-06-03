using Scientific_Equipment.MoreWindows;
using Scientific_Equipment.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Equipment.ViewModels
{
    class MainVM: BaseVM
    {
      
        
        public CommandVM Login { get; set; }

        public MainVM()
        {

            
            Login = new CommandVM(() => { new MainWindow().Show(); });
        }
    }
}
