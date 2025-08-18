using Applicacao.DTOs;

namespace Applicacao.Interfaces;


public interface ISvcObterUsuario
{
    public Task<IEnumerable<DtoUsuarioSistema>> Obter(DtoUsuarioSistema dto);

}
