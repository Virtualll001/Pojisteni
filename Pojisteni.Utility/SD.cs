using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pojisteni.Utility
{
    public static class SD
    {
        //textové řetězce ideálně kopírovat z migrace:
        public const string Proc_Pojistka_Create = "usp_CreatePojistky";
        public const string Proc_Pojistka_Get = "usp_GetPojistku";
        public const string Proc_Pojistka_GetAll = "usp_GetPojistky";
        public const string Proc_Pojistka_Update = "usp_UpdatePojistky";
        public const string Proc_Pojistka_Delete = "usp_DeletePojistky";
    }
}
