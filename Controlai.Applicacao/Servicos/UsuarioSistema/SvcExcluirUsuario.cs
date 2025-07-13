namespace Applicacao.Servicos;

using System;
using Dominio.Interfaces;
using Applicacao.DTOs;
using Dominio.Enums;


public class SvcExcluirUsuario
{
    private readonly IRepoUsuarioSistema _usuarioSistemaRepo;
    private readonly SvcObterUsuario _obterusuario;

    public SvcExcluirUsuario(IRepoUsuarioSistema usuarioSistemaRepo, SvcObterUsuario obterusuario)
    {
        _usuarioSistemaRepo = usuarioSistemaRepo;
        _obterusuario = obterusuario;
    }
    
    public async Task ExcluirUsuario(DtoUsuarioSistema dtousuarioSistema)
    {
        if
        (   string.IsNullOrWhiteSpace(dtousuarioSistema.Usuario) ||
            string.IsNullOrWhiteSpace(dtousuarioSistema.Email) ||
            string.IsNullOrWhiteSpace(dtousuarioSistema.Nome)
        )   throw new Exception("Parâmetros insuficientes para exclusão de usuário"); 

        dtousuarioSistema.Busca = TipoDeBusca.PorId;

        List<DtoUsuarioSistema> usuarioList = (await _obterusuario.Obter(dtousuarioSistema)).ToList();
        if (usuarioList == null || !usuarioList.Any()) throw new Exception("Usuário não informado");

        DtoUsuarioSistema usuario = usuarioList.First();

        bool iguais = SvcComparador.CompararObjetos(usuario, dtousuarioSistema);
        
        if (!iguais) throw new Exception("Os dados informados diferem do registro atual do usuário. Exclusão cancelada por segurança.");

        await _usuarioSistemaRepo.ExcluirUsuario(dtousuarioSistema.Id);
    }
}