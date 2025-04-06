using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Clientes.Models;
using Clientes.DATA;

namespace Clientes.Controllers;


[Route("api/controller")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly ClientesData _ClienteData;
    public ClientesController(ClientesData clientesData)
    {
        _ClienteData = clientesData;
    }
    [HttpGet()]
    public async Task<IActionResult> Lista()
    {
        List<Cliente> Lista = await _ClienteData.Lista();
        return StatusCode(StatusCodes.Status200OK, Lista);
    }

    [HttpGet("{cliente_id}")]
    public async Task<IActionResult> Obtener(int cliente_id)
    {
        Cliente objeto = await _ClienteData.Obtener(cliente_id);
        return StatusCode(StatusCodes.Status200OK, objeto);
    }

    [HttpPost]
    public async Task<IActionResult> Agregar([FromBody] Cliente objeto)
    {
        bool respuesta = await _ClienteData.Agregar(objeto);
        return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
    }

    [HttpPut]
    public async Task<IActionResult> Editar([FromBody] Cliente objeto)
    {
        bool respuesta = await _ClienteData.Editar(objeto);
        return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
    }

    [HttpDelete("{nit}")]
    public async Task<IActionResult> Eliminar(int nit)
    {
        bool respuesta = await _ClienteData.Eliminar(nit);
        return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
    }
}