using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Models;
using Applicacao.DTOs;
using System.Text.RegularExpressions;


namespace Applicacao.Servicos
{
    public class SvcTransformacao
    {

        public static UsuarioSistema DtoForUsuarioSistema(DtoUsuarioSistema usuarioforedit)
        {
            if (string.IsNullOrWhiteSpace(usuarioforedit.Nome)) throw new Exception("Nome invalido");
            if (string.IsNullOrWhiteSpace(usuarioforedit.Usuario)) throw new Exception("Usuario invalido");
            if (!Regex.IsMatch(usuarioforedit.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    throw new ArgumentException("Email inválido");
            

            if (string.IsNullOrWhiteSpace(usuarioforedit.Senha))
            {
                UsuarioSistema UsuarioEditado = new UsuarioSistema
                (
                Nome: usuarioforedit.Nome,
                Usuario: usuarioforedit.Usuario,
                Email: usuarioforedit.Email
                )
                {
                    isAdministrador = usuarioforedit.isAdministrador,
                    DataDeCadastro = usuarioforedit.DataDeCadastro,
                    Id = usuarioforedit.Id
                };

                return UsuarioEditado;
            }
            
            else if (!string.IsNullOrWhiteSpace(usuarioforedit.Senha))
            {


                UsuarioSistema UsuarioEditado = new UsuarioSistema
                (
                Nome: usuarioforedit.Nome,
                Usuario: usuarioforedit.Usuario,
                Email: usuarioforedit.Email,
                SenhaHash: usuarioforedit.Senha
                )
                {
                    isAdministrador = usuarioforedit.isAdministrador,
                    DataDeCadastro = usuarioforedit.DataDeCadastro,
                    Id = usuarioforedit.Id
                };

                return UsuarioEditado;
            }


            throw new Exception("Alteração inválida");


        }


        public static DtoUsuarioSistema UsuarioSistemaforDto(UsuarioSistema usuarioforedit)
        {

            if (usuarioforedit.Usuario == null || usuarioforedit.Nome == null || usuarioforedit.Email == null)
                throw new Exception("Usuario precisa de dados");
            
            if (!Regex.IsMatch(usuarioforedit.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new Exception("Necessário informar um e-mail válido");
            }

            DtoUsuarioSistema usuarioSistema = new DtoUsuarioSistema
            (
            Nome: usuarioforedit.Nome,
            Usuario: usuarioforedit.Usuario,
            Email: usuarioforedit.Email
            )
            {
                isAdministrador = usuarioforedit.isAdministrador,
                DataDeCadastro = usuarioforedit.DataDeCadastro,
                Id = usuarioforedit.Id
            };

            return usuarioSistema;
        }
    }
}