using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaszynaPi.MachineLogic.Architecture;

namespace MaszynaPi.MachineUI {
    public partial class UserControlIntButton : Button {

        public uint InterruptPriority { get; set; }
        Register InterruptRequestRegisterHandle;
        
        public static Action OnSetRequestValue;

        public UserControlIntButton() {
            InitializeComponent();   
        }


        public void SetIntRequestRegisterHandle(Register RZ) {
            InterruptRequestRegisterHandle = RZ;
        }

        public void SetIntRequestValue() {
            InterruptRequestRegisterHandle.SetValue(InterruptRequestRegisterHandle.GetValue() | InterruptPriority);
        }

        protected override void OnClick(EventArgs e) {
            base.OnClick(e);
            SetIntRequestValue();
            OnSetRequestValue();
        }
    }
}
