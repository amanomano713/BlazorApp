using DevExpress.Xpo;

namespace BlazorApp.Entities.Xpo
{

    //No quitar XPLiteObject and Persistent
    [Persistent("MovPackage")]
    public class MovPackage : XPLiteObject
    {

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public MovPackage(Session session) : base(session) { }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public MovPackage() : base(XpoDefault.Session) { }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>(nameof(Id), ref fId, value); }
        }

        string fIdAfiliado;
        [Nullable(false)]
        public string IdAfiliado
        {
            get { return fIdAfiliado; }
            set { SetPropertyValue<string>(nameof(IdAfiliado), ref fIdAfiliado, value); }
        }

        DateTime fDateCreated;
        [Nullable(false)]
        public DateTime DateCreated
        {
            get { return fDateCreated; }
            set { SetPropertyValue<DateTime>(nameof(DateCreated), ref fDateCreated, value); }
        }

        int fIdPackage;
        [Nullable(false)]
        public int IdPackage
        {
            get { return fIdPackage; }
            set { SetPropertyValue<int>(nameof(IdPackage), ref fIdPackage, value); }
        }

        string fCodPackage;
        [Nullable(false)]
        public string CodPackage
        {
            get { return fCodPackage; }
            set { SetPropertyValue<string>(nameof(CodPackage), ref fCodPackage, value); }
        }

        float fInteres;
        [Nullable(false)]
        public float Interes
        {
            get { return fInteres; }
            set { SetPropertyValue<float>(nameof(Interes), ref fInteres, value); }
        }

        float fPorcentaje;
        [Nullable(false)]
        public float Porcentaje
        {
            get { return fPorcentaje; }
            set { SetPropertyValue<float>(nameof(Porcentaje), ref fPorcentaje, value); }
        }

        float fMontoPackage;
        [Nullable(false)]
        public float MontoPackage
        {
            get { return fMontoPackage; }
            set { SetPropertyValue<float>(nameof(MontoPackage), ref fMontoPackage, value); }
        }
        float fMontoRetiro;
        [Nullable(false)]
        public float MontoRetiro
        {
            get { return fMontoRetiro; }
            set { SetPropertyValue<float>(nameof(MontoRetiro), ref fMontoRetiro, value); }
        }
        float fMontoTransferido;
        [Nullable(false)]
        public float MontoTransferido
        {
            get { return fMontoTransferido; }
            set { SetPropertyValue<float>(nameof(MontoTransferido), ref fMontoTransferido, value); }
        }
    }
}
