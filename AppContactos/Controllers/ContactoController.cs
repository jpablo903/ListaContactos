using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AppContactos.Models;
using System.Data.SqlClient;
using System.Data;

namespace AppContactos.Controllers
{
    
    public class ContactoController : Controller
    {
        private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();

        private static List<Contacto> listaContacto = new List<Contacto>();
        // GET: Contacto
        public ActionResult Inicio()
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                listaContacto = new List<Contacto>();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Contactos", oconexion);
                cmd.CommandType = CommandType.Text;
                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader()) {
                    while (dr.Read()) {
                        Contacto contacto = new Contacto();
                        contacto.IdContacto = Convert.ToInt32(dr["IdContacto"]);
                        contacto.Nombre = dr["Nombre"].ToString();
                        contacto.Apellido = dr["Apellido"].ToString();
                        contacto.Telefono = dr["Telefono"].ToString();
                        contacto.Correo = dr["Correo"].ToString();

                        listaContacto.Add(contacto);
                    }
                }


            }
            return View(listaContacto);
        }
        [HttpGet]
        public ActionResult Registrar()
        {

            return View();
        }


        [HttpGet]
        public ActionResult Editar(int? idcontacto)
        {
            if (idcontacto == null) 
                return RedirectToAction("Inicio", "Contacto");

            Contacto contacto = listaContacto.Where(c => c.IdContacto == idcontacto).FirstOrDefault();

            return View(contacto);
        }


        [HttpGet]
        public ActionResult Eliminar(int? idcontacto)
        {
            if (idcontacto == null)
                return RedirectToAction("Inicio", "Contacto");

            Contacto contacto = listaContacto.Where(c => c.IdContacto == idcontacto).FirstOrDefault();

            return View(contacto);
        }

        [HttpPost]
        public ActionResult Registrar(Contacto contacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                listaContacto = new List<Contacto>();

                SqlCommand cmd = new SqlCommand("SP_Registrar", oconexion);
                cmd.Parameters.AddWithValue("Nombre", contacto.Nombre);
                cmd.Parameters.AddWithValue("Apellido", contacto.Apellido);
                cmd.Parameters.AddWithValue("Telefono", contacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", contacto.Correo);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();

            }

            return RedirectToAction("Inicio", "Contacto");
        }

        [HttpPost]
        public ActionResult Editar(Contacto contacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SP_Editar", oconexion);
                cmd.Parameters.AddWithValue("IdContacto", contacto.IdContacto);
                cmd.Parameters.AddWithValue("Nombre", contacto.Nombre);
                cmd.Parameters.AddWithValue("Apellido", contacto.Apellido);
                cmd.Parameters.AddWithValue("Telefono", contacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", contacto.Correo);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();

            }

            return RedirectToAction("Inicio", "Contacto");
        }

        [HttpPost]
        public ActionResult Eliminar(string IdContacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SP_Eliminar", oconexion);
                cmd.Parameters.AddWithValue("IdContacto", IdContacto);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();

            }

            return RedirectToAction("Inicio", "Contacto");
        }
    }
}