namespace Applicacao.Servicos;

using System;
using System.Linq;
using System.Threading.Tasks;
using Applicacao.DTOs;
using Dominio.Enums;


public class SvcAutenticarUsuario
{
    private readonly SvcObterUsuario _obterusuario;

    public SvcAutenticarUsuario (SvcObterUsuario svcObterUsuario)
    {
        _obterusuario = svcObterUsuario;
    }

    public async Task<bool> AutenticarUsuario(DtoUsuarioSistema dtoUsuarioSistema)
    {
        if (string.IsNullOrWhiteSpace(dtoUsuarioSistema.Senha)) throw new Exception("Senha precisa ter conteúdo");
        dtoUsuarioSistema.Busca = TipoDeBusca.PorQualquer;

        var listUsuario = await _obterusuario.Obter(dtoUsuarioSistema);
        var usuario = listUsuario.FirstOrDefault();

        if (usuario == null) throw new Exception("Usuário não encontrado");
        if (usuario.Senha == null) throw new Exception("Senha não encontrada");

        return SvcCryptSenha.VerificarHash(dtoUsuarioSistema.Senha, usuario.Senha);
    }
            
}
