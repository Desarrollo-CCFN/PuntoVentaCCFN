namespace Capa_Entidad
{
    public class CE_Usuarios
    {

        private int _USERID;
        private string _USER_CODE;
        private string _U_NAME;
        private string _DfltsGroup;
        private string _Locked;

        public int USERID { get => _USERID; set => _USERID = value; }
        public string USER_CODE { get => _USER_CODE; set => _USER_CODE = value; }
        public string U_NAME { get => _U_NAME; set => _U_NAME = value; }
        public string DfltsGroup { get => _DfltsGroup; set => _DfltsGroup = value; }
        public string Locked { get => _Locked; set => _Locked = value; }
    }
}
