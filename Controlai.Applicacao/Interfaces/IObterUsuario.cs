using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Applicacao.DTOs;
using Applicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.Enums;
using System.Linq;
namespace Applicacao.Interfaces;


public interface ISvcObterUsuario
{
    public Task<IEnumerable<DtoUsuarioSistema>> Obter(DtoUsuarioSistema dto);
    
}
