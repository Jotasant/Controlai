namespace Web.Rest;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Applicacao.DTOs;
using Applicacao.Servicos;



[ApiController]
[Route("api/[controller]")]
public class APIControllai : ControllerBase
{
    private SvcCriarUsuario _svcCriarUsuario;
    public APIControllai(SvcCriarUsuario svcCriarUsuario)
    {
        _svcCriarUsuario = svcCriarUsuario;
    }
    [HttpPost]
    public async Task<ActionResult<DtoUsuarioSistema>> CriarUsuario([FromBody] DtoUsuarioSistema usuarioSistema)
    {

        var usuario =  await _svcCriarUsuario.CriarUsuario(usuarioSistema);

        return usuario;
    }

}
