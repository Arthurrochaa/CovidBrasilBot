using System;
using System.Collections.Generic;
using System.Text;

namespace CoronavirusBrasil.Models
{
    public class Response
    {
        public string Country { get; set; }
        public Cases Cases { get; set; }
        public Deaths Deaths { get; set; }
        public string Day { get; set; }

        public override string ToString()
        {
            return
                $"Novos casos: {Cases.New} \n" +
                $"Total de casos: {Cases.Total} \n" +
                $"Novas mortes: {Deaths.New} \n" +
                $"Total de mortes: {Deaths.Total} \n" +
                $"Última Atualização: {Day}";
        }
    }
}
