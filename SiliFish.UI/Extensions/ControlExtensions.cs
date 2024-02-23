namespace Extensions
{
    public static class ControlExtensions
    {
        public static void SetEnabled(this Control control, bool enabled)
        {
            control.Enabled = enabled;
            foreach (Control ctrl in control.Controls)
            {
                ctrl.SetEnabled(enabled);
            }
        }

        //if the control is a container, the enabled fields of the children is not updated
        public static void SetEnabledNoChild(this Control control, bool enabled)
        {
            Dictionary<string, bool> prevValues= [];
            foreach (Control ctrl in control.Controls)
            {
                prevValues[ctrl.Name] = ctrl.Enabled;
            }
            control.Enabled = enabled;
            foreach (Control ctrl in control.Controls)
            {
                ctrl.SetEnabledNoChild(prevValues[ctrl.Name]);
            }
        }

    }
}
