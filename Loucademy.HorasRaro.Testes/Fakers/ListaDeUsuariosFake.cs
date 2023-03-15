using Bogus;
using Loucademy.HorasRaro.Domain;
using Loucademy.HorasRaro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loucademy.HorasRaro.Testes.Fakers
{
    public static class ListaDeUsuariosFake
    {
        private static readonly Faker Fake = new Faker();
        public static async Task<IEnumerable<Usuario>> UsuarioEntitiesAsync()
        {
            var minhaLista = new List<Usuario>();

            for (int i = 0; i < 5; i++)
            {
                minhaLista.Add(new Usuario()
                {
                    Id = i,
                    Nome = Fake.Person.FirstName,
                    Email = Fake.Person.Email,
                    Ativo = true,
                    DataCriacao = DateTime.Now,
                    Role = Fake.PickRandom<EnumRole>()

                }) ;
            }

            return minhaLista;
        }


    }
}
