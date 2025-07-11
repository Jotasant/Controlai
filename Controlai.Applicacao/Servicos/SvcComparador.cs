using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controlai.Applicacao.Servicos
{
    public class SvcComparador
    {
        public static bool CompararObjetos<T>(T obj1, T obj2)
        {
            var propriedades = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var prop in propriedades)
            {
                var valorAtual = prop.GetValue(obj1);
                var valorNovo = prop.GetValue(obj2);

                if (valorAtual == null && valorNovo == null) continue;
                if (valorAtual == null || valorNovo == null) return false;

                if (!valorAtual.Equals(valorNovo))
                {
                    return false;
                }
            }

            return true;
        }
    }
}