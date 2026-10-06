using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartClinicAppointment.Models;
using Microsoft.AspNetCore.Mvc.Rendering; // مهم عشان SelectList

public class AppointmentsController : Controller
{
    private readonly ClinicDbContext _context;

    public AppointmentsController(ClinicDbContext context)
    {
        _context = context;
    }

    // GET: APPOINTMENTS
    public async Task<IActionResult> Index()
    {
        return View(await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .ToListAsync());
    }

    // GET: APPOINTMENTS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (appointment == null)
        {
            return NotFound();
        }

        return View(appointment);
    }

    // GET: APPOINTMENTS/Create
    public IActionResult Create()
    {
        ViewBag.DoctorId = new SelectList(_context.Doctors, "Id", "FullName");
        ViewBag.PatientId = new SelectList(_context.Patients, "Id", "FullName");
        return View();
    }

    // POST: APPOINTMENTS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,PatientId,DoctorId,AppointmentDate,AppointmentTime,Notes,Status,CreatedAt,UpdatedAt")] Appointment appointment)
    {
        if (ModelState.IsValid)
        {
            _context.Add(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.DoctorId = new SelectList(_context.Doctors, "Id", "FullName", appointment.DoctorId);
        ViewBag.PatientId = new SelectList(_context.Patients, "Id", "FullName", appointment.PatientId);
        return View(appointment);
    }


    // GET: APPOINTMENTS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }

        ViewBag.DoctorId = new SelectList(_context.Doctors, "Id", "FullName", appointment.DoctorId);
        ViewBag.PatientId = new SelectList(_context.Patients, "Id", "FullName", appointment.PatientId);

        return View(appointment);
    }

    // POST: APPOINTMENTS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,PatientId,DoctorId,AppointmentDate,AppointmentTime,Notes,Status,CreatedAt,UpdatedAt")] Appointment appointment)
    {
        if (id != appointment.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(appointment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(appointment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.DoctorId = new SelectList(_context.Doctors, "Id", "FullName", appointment.DoctorId);
        ViewBag.PatientId = new SelectList(_context.Patients, "Id", "FullName", appointment.PatientId);

        return View(appointment);
    }

    // GET: APPOINTMENTS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (appointment == null)
        {
            return NotFound();
        }

        return View(appointment);
    }

    // POST: APPOINTMENTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AppointmentExists(int? id)
    {
        return _context.Appointments.Any(e => e.Id == id);
    }
}
