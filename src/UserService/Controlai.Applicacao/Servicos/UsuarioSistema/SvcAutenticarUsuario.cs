namespace Applicacao.Servicos;

using System;
using System.Linq;
using System.Threading.Tasks;
using Applicacao.DTOs;
using Applicacao.Interfaces;
using Dominio.Enums;



public class SvcAutenticarUsuario
{
    private readonly ISvcObterUsuario _obterusuario;

    public SvcAutenticarUsuario (ISvcObterUsuario svcObterUsuario)
    {
        _obterusuario = svcObterUsuario;
    }

    public async Task<DtoUsuarioSistema> AutenticarUsuario(DtoUsuarioSistema dtoUsuarioSistema)
    {
        if (string.IsNullOrWhiteSpace(dtoUsuarioSistema.Senha)) throw new Exception("Senha precisa ter conteúdo");
        dtoUsuarioSistema.Busca = TipoDeBusca.PorEmail;

        var listUsuario = await _obterusuario.Obter(dtoUsuarioSistema);
        var usuario = listUsuario.FirstOrDefault();

        if (usuario == null) throw new Exception("Usuário não encontrado");
        if (usuario.Senha == null) throw new Exception("Senha não encontrada");

        if (SvcCryptSenha.VerificarHash(dtoUsuarioSistema.Senha, usuario.Senha)) return usuario;

        throw new Exception("Credenciais incorretas");

    }
            
}
