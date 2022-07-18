using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace MoneySave
{
    internal class CUsuario
    {
        GestorDeGastosEntities2 db;
        public void Create(Usuario pUsuario)
        {
            try
            {
                using (db = new GestorDeGastosEntities2())
                {
                    db.Usuarios.Add(pUsuario);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public List<Usuario> Read()
        {
            try
            {
                using (db = new GestorDeGastosEntities2())
                {
                    return db.Usuarios.ToList();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public void Update(Usuario pUsuario)
        {
            try
            {
                using (db = new GestorDeGastosEntities2())
                {
                    db.Entry(pUsuario).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public void Delete(int pId)
        {
            try
            {
                using (db = new GestorDeGastosEntities2())
                {
                    db.Usuarios.Remove(db.Usuarios.Single(p => p.IdUsuario == pId));
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public List<Usuario> buscarId(int pId)
        {
            try
            {
                using (db = new GestorDeGastosEntities2())
                {
                    return db.Usuarios.Where(p => p.IdUsuario == pId).ToList();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
