using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caelestia_Simul
{
    class clsTime
    {
        public DateTime getUTC()
        {
            return DateTime.UtcNow;
        }

        public DateTime getLocal()
        {
            return DateTime.Now;
        }

        public double getUTCJulianDate()
        {
            DateTime date = DateTime.UtcNow;
            return date.ToOADate() + 2415018.5;
        }

        public double getLocalJulianDate()
        {
            DateTime date = DateTime.Now;
            return date.ToOADate() + 2415018.5;
        }

        public DateTime getGegorianFromJulian(string julianDate)
        {
            // TODO add validation logic, plus rules to cope with pre-2000 dates
            var yy = 2000 + int.Parse(julianDate.Substring(0, 2));
            var ddd = int.Parse(julianDate.Substring(2).Split('.').First());
            return new DateTime(yy, 1, 1).AddDays(ddd);
        }

        public double calculateGMST()
        {
            // GMST = 6.697374558 + 0.06570982441908 * D0 + 1.00273790935 * H + 0.000026 * T^2
            double julianDate = getUTCJulianDate();

            double midnight = Math.Floor(julianDate) - 0.5;
            double days_since_midnight = julianDate - midnight;
            double hours_since_midnight = days_since_midnight * 24;
            double days_since_epoch = julianDate - 2451545.0;
            double centuries_since_epoch = days_since_epoch / 36525;
            double whole_days_since_epoch = midnight - 2451545.0;

            double GMST = 6.697374558 + 0.06570982441908 * whole_days_since_epoch + 1.00273790935 * hours_since_midnight + 0.000026 * (centuries_since_epoch * centuries_since_epoch);
            return GMST;
        }

        public string getGMST()
        {
            double GMST = calculateGMST();

            long GMST_trunc = (long)GMST;
            double GMST_hours = GMST_trunc % 24;

            double GMST_min = GMST - GMST_trunc;
            double GMST_min1 = GMST_min * 60;
            double GMST_minutes = Math.Floor(GMST_min1);

            double GMST_sec = GMST_minutes - GMST_min;
            double GMST_seconds = Math.Floor(GMST_sec * 60);

            return GMST_hours + ":" + GMST_minutes + ":" + GMST_seconds;
        }

        public double calculateGAST()
        {
            // GAST = GMST + eqeq
            // eqeq = Δψ * cos ε
            // Δψ ≈ -0.000319 * sin Ω - 0.000024 * sin 2L
            // Ω = 125.04 - 0.052954 * D
            // L = 280.47 + 0.98565 * D
            // ε = 23.4393 - 0.0000004 * D

            double julianDate = getUTCJulianDate();
            double midnight = Math.Floor(julianDate) - 0.5;
            double D = midnight - 2451545.0;
            double Ω = 125.04 - 0.052954 * D;
            double L = 280.47 + 0.98565 * D;
            double ε = 23.4393 - 0.0000004 * D;

            double Δψ = -0.000319 * Math.Sin(Ω) - 0.000024 * Math.Sin(2L);

            double eqeq = Δψ * Math.Cos(ε);

            double GAST = calculateGMST() + eqeq;

            return GAST;
        }

        public string getGAST()
        {
            double GAST = calculateGAST();

            long GAST_trunc = (long)GAST;
            double GAST_hours = GAST_trunc % 24;

            double GAST_min = GAST - GAST_trunc;
            double GAST_min1 = GAST_min * 60;
            double GAST_minutes = Math.Floor(GAST_min1);

            double GAST_sec = GAST_minutes - GAST_min;
            double GAST_seconds = Math.Floor(GAST_sec * 60);

            return GAST_hours + ":" + GAST_minutes + ":" + GAST_seconds;
        }
    }
}
