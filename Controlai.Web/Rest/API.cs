namespace Web.Rest;


using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Applicacao.DTOs;
using Applicacao.Servicos;



[ApiController]
[Route("api/[controller]")]

public class ControllaiController : ControllerBase
{
    private readonly SvcCriarUsuario _svcCriarUsuario;
    private readonly SvcExcluirUsuario _svcExcluirUsuario;
    private readonly SvcAutenticarUsuario _svcAutenticarUsuario;
    private readonly SvcEditarUsuario _svcEditarUsuario;
    public ControllaiController(SvcCriarUsuario svcCriarUsuario, SvcExcluirUsuario svcExcluirUsuario, SvcAutenticarUsuario svcAutenticarUsuario, SvcEditarUsuario svcEditarUsuario)
    {
        //Manipular Usuário
        _svcCriarUsuario = svcCriarUsuario;
        _svcEditarUsuario = svcEditarUsuario;
        _svcExcluirUsuario = svcExcluirUsuario;
        _svcAutenticarUsuario = svcAutenticarUsuario;
        
    }

    [HttpPost("CriarUsuario")]
    public async Task<ActionResult<DtoUsuarioSistema>> CriarUsuario([FromBody] DtoUsuarioSistema dtousuarioSistema)
    {
        var usuario = await _svcCriarUsuario.CriarUsuario(dtousuarioSistema);

        return usuario;
    }

    [HttpPost("EditarUsuario")]
    public async Task<ActionResult<DtoUsuarioSistema>> EditarUsuario([FromBody] DtoUsuarioSistema dtoUsuarioSistema)
    {
        var usuario = await _svcEditarUsuario.EditarUsuario(dtoUsuarioSistema);
        return usuario;
    }

    [HttpPost("ExcluirUsuario")]
    public async Task<ActionResult<DtoUsuarioSistema>> ExcluirUsuario([FromBody] DtoUsuarioSistema dtousuarioSistema)
    {
        await _svcExcluirUsuario.ExcluirUsuario(dtousuarioSistema);

        return Ok();
    }

    [HttpPost("Autenticar")]
    public async Task<ActionResult<DtoUsuarioSistema>> AutenticarUsuario([FromBody] DtoUsuarioSistema dtousuarioSistema)
    {
        var usuario = await _svcAutenticarUsuario.AutenticarUsuario(dtousuarioSistema);

        return usuario;
    }


}
