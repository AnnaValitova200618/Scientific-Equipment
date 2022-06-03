using Scientific_Equipment.DTO;
using Scientific_Equipment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Equipment.ViewModels
{
    class ScientistsVM: BaseVM
    {
        private List<Scientists> scientists;
        public List<Scientists> Scientists
        {
            get => scientists;
            set
            {
                scientists = value;
                Signal();
            }
        }
        public Scientists EditScientists { get; set; }
        private Scientists selectedScienisis;
        public Scientists SelectedScienisis
        {
            get => selectedScienisis;
            set
            {
                selectedScienisis = value;
                if (selectedScienisis == null)
                    return;
                EditScientists = new Scientists
                {
                    firstname = selectedScienisis.firstname,
                    patronymic = selectedScienisis.patronymic,
                    lastname = selectedScienisis.lastname,
                    login = selectedScienisis.login,
                    password = selectedScienisis.password,
                    position = selectedScienisis.position
                };
                Signal(nameof(EditScientists));
            }
        }

        public ScientistsVM()
        {
            var db = SqlModel.GetInstance();
            Scientists = SqlModel.GetInstance().ListScientists();
            EditScientists = new Scientists { };
        }

    }
}
