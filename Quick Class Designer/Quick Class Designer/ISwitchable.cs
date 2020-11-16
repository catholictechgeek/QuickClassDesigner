using System;
using System.Collections.Generic;

namespace QuickClassDesigner
{
    public delegate void PanelClosedHandler(object sender, EventArgs args);

    public interface ISwitchable
    {
        //void resetModel();
        void initModel();
        void initExistingModel(IGeneratable data);
        //void OnPanelCosed();

    }
}
