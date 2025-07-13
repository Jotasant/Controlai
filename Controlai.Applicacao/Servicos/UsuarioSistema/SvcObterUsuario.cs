namespace Applicacao.Servicos;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Applicacao.DTOs;
using Applicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.Enums;
using System.Linq;


public class SvcObterUsuario : ISvcObterUsuario
{
    
    private readonly IRepoUsuarioSistema _usuarioSistemaRepo;

    public SvcObterUsuario (IRepoUsuarioSistema usuarioSistemaRepo)
    {
        _usuarioSistemaRepo = usuarioSistemaRepo;
    }

    public async Task<IEnumerable<DtoUsuarioSistema>> Obter(DtoUsuarioSistema dto)
    {

        var usuarios = new List<UsuarioSistema>();
        var usuarioDtoList = new List<DtoUsuarioSistema>();


        switch (dto.Busca)
        {

            case TipoDeBusca.PorNome:

                if (string.IsNullOrWhiteSpace(dto.Nome)) throw new ArgumentException("Nome é obrigatório.");
                usuarios = await _usuarioSistemaRepo.ObterPorNome(dto.Nome);
                break;

            case TipoDeBusca.PorUsuario:

                if (string.IsNullOrWhiteSpace(dto.Usuario)) throw new ArgumentException("Usuário é obrigatório.");
                var usuarioPorUsuario = await _usuarioSistemaRepo.ObterPorUsuario(dto.Usuario);
                if (usuarioPorUsuario != null) usuarios.Add(usuarioPorUsuario);
                break;

            case TipoDeBusca.PorEmail:

                if (string.IsNullOrWhiteSpace(dto.Email)) throw new ArgumentException("Email é obrigatório.");
                var usuarioPorEmail = await _usuarioSistemaRepo.ObterPorEmail(dto.Email);
                if (usuarioPorEmail != null) usuarios.Add(usuarioPorEmail);
                break;

            case TipoDeBusca.PorId:
                if (dto.Id <= 0) throw new ArgumentException("ID inválido.");
                var usuarioPorId = await _usuarioSistemaRepo.ObterPorId(dto.Id);
                if (usuarioPorId != null) usuarios.Add(usuarioPorId);
                break;

            case TipoDeBusca.PorDataDeCadastro:
                if (dto.DataDeCadastro == DateTime.MinValue) throw new ArgumentException("Data de cadastro é obrigatória.");
                usuarios = await _usuarioSistemaRepo.ObterPorDataDeCadastro(dto.DataDeCadastro.Date);
                break;

            case TipoDeBusca.PorPerfil:
                usuarios = await _usuarioSistemaRepo.ObterPorPerfil(dto.isAdministrador);
                break;

            case TipoDeBusca.Todos:
                usuarios = await _usuarioSistemaRepo.ObterTodos();
                break;

            case TipoDeBusca.PorQualquer:

                //Por Usuario
                if (dto.Usuario != null)
                {
                    var usuarioPorUsuarioQualquer = await _usuarioSistemaRepo.ObterPorUsuario(dto.Usuario);
                    if (usuarioPorUsuarioQualquer != null) usuarios.Add(usuarioPorUsuarioQualquer);
                }
                else if (dto.Email != null)
                {
                    var usuarioPorEmailQualquer = await _usuarioSistemaRepo.ObterPorEmail(dto.Email);
                    if (usuarioPorEmailQualquer != null) usuarios.Add(usuarioPorEmailQualquer);
                }
                break;

            default:
                throw new ArgumentException("Tipo de busca inválido.");
        }

        return usuarios.Select(u => MapearUsuario(u)).ToList();
        
        /*
        foreach (var us in usuarios)
        {
            var usuarioDto = MapearUsuario(us);
            usuarioDtoList.Add(usuarioDto);
        }

        return usuarioDtoList;
        */
    }


    private DtoUsuarioSistema MapearUsuario(UsuarioSistema usuariosistema)
    {
        return new DtoUsuarioSistema
        (
            Id: usuariosistema.Id,
            Nome: usuariosistema.Nome!,
            Usuario: usuariosistema.Usuario!,
            Senha: usuariosistema.SenhaHash!,
            Email: usuariosistema.Email!
            )
        {
            isAdministrador = usuariosistema.isAdministrador,
            DataDeCadastro = usuariosistema.DataDeCadastro
        };
    }
}
