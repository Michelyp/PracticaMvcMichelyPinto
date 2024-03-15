#region PROCEDURE

//alter procedure SP_ZAPAIMAGENES(@IDPRODUCTO int, @POSICION int, @registros int out)
//as 
//select @registros = count(IDIMAGEN) from IMAGENESZAPASPRACTICA
//where IDPRODUCTO = @IDPRODUCTO
//select IDIMAGEN, IDPRODUCTO, IMAGEN from 
//	(select cast(
//	row_number() over (order by IDIMAGEN) as int) as posicion
//	, ISNULL(IDIMAGEN, 0) AS IDIMAGEN, IDPRODUCTO, IMAGEN
//	from IMAGENESZAPASPRACTICA
//	where IDPRODUCTO = @IDPRODUCTO) as query
//	where query.posicion >= @POSICION and query.posicion < (@POSICION + 1)
//go

//create procedure SP_INSERT_IMAGEN
//(@imagen nvarchar(150))
//as
//    declare @maxinscripcion int
//    select @maxinscripcion = max(IDIMAGEN) + 1 from IMAGENESZAPASPRACTICA
//    insert into IMAGENESZAPASPRACTICA values (@maxinscripcion, @imagen,1)
//go
#endregion
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PracticaMvcMichelyPinto.Data;
using PracticaMvcMichelyPinto.Models;
using System.Data;

namespace PracticaMvcMichelyPinto.Repositories
{
    public class RepositoryZapatilla
    {
        private ZapatillaContext context;
        public RepositoryZapatilla (ZapatillaContext context)
        {
            this.context = context;
        }
        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            return await this.context.Zapatillas.ToListAsync();
        }
        public async Task<Zapatilla> FindZapatillaAsync(int idproducto)
        {
            return await this.context.Zapatillas
                .FirstOrDefaultAsync(x => x.IdProducto == idproducto);
        }
        public async Task<int> GetNumeroRegistrosZapatillas(int idproducto)
        {
            return await this.context.ImagenZapatillas.Where(z => z.IdProducto == idproducto).CountAsync();
        }
        public async Task<ModelPaginacion> GetImagen(int idproducto, int posicion)
        {
            string sql = "SP_ZAPAIMAGENES @IDPRODUCTO @POSICION";
            SqlParameter pamId =
                new SqlParameter("@IDPRODUCTO", idproducto);
            SqlParameter pamPosicion =
                new SqlParameter("@POSICION", posicion);
            SqlParameter pamRegistros = new SqlParameter("@registros", -1);

            var consulta = this.context.ImagenZapatillas.FromSqlRaw
                (sql, pamId, pamPosicion,pamRegistros);
            List<ImagenZapatilla> imagenes = await consulta.ToListAsync();
            int registros = (int)pamRegistros.Value;
            return new ModelPaginacion
            {
                NumeroRegistros = registros,
                Imagenes = imagenes

            };
        }
        public void InsertarImagen
           ( string imagen)
        {
            string sql = "SP_ZAPAIMAGENES @imagen";
            SqlParameter pamimagen =
                 new SqlParameter("@IDPRODUCTO", imagen);
            this.context.Database.ExecuteSqlRaw(sql, pamimagen);
        }


    }
}
