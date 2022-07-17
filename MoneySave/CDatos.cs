using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Data.Entity;

namespace MoneySave
{
    public class CDatos
    {
        GestorDeGastosEntities1 db;
        public void Create(Cuenta pCuenta)
        {
            try
            {
                using (db = new GestorDeGastosEntities1())
                {
                    db.Cuentas.Add(pCuenta);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
        public List<Cuenta> Read()
        {
            try
            {
                using (db = new GestorDeGastosEntities1())
                {
                    return db.Cuentas.ToList();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public void Update(Cuenta pCuenta)
        {
            try
            {
                using (db = new GestorDeGastosEntities1())
                {
                    db.Entry(pCuenta).State=EntityState.Modified;
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
                using (db = new GestorDeGastosEntities1())
                {
                    db.Cuentas.Remove(db.Cuentas.Single(p => p.IdCuenta == pId));
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public List<Cuenta> buscarId(int pId)
        {
            try
            {
                using (db = new GestorDeGastosEntities1())
                {
                    return db.Cuentas.Where(p=> p.IdCuenta == pId).ToList();
                    
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
