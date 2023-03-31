using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using twitter_clone.Models;

namespace twitter_clone.controllers;

public class ReportController
{
    public static async Task<IResult> GetReports(TwitterCloneContext db)
    {
        var reports = await db.Reports.ToListAsync();
        return Utils.Response("", reports, 0, HttpStatusCode.OK);
    }

    public static async Task<IResult> GetReport(int reportId, TwitterCloneContext db)
    {
        var existingReport = await db.Reports.FindAsync(reportId);
        if (existingReport == null)
        {
            return Utils.Response("Report not found", "", 0, HttpStatusCode.NotFound);
        }

        return Utils.Response("", existingReport, 1, HttpStatusCode.OK);
    }

    public static async Task<IResult> CreateReport(
        [FromBody] Report report,
        User user,
        TwitterCloneContext db
    )
    {
        db.Reports.Add(new Report(post: report.Post, description: report.Description, user: user));
        await db.SaveChangesAsync();

        return Utils.Response("", "", 0, HttpStatusCode.OK);
    }

    public static async Task<IResult> UpdateReport(
        [FromBody] Report report,
        User user,
        TwitterCloneContext db
    )
    {
        var existingReport = await db.Reports.FindAsync(report.Id);
        if (existingReport == null)
        {
            return Utils.Response("Report does not exist", "", 0, HttpStatusCode.NotFound);
        }

        existingReport.Description = report.Description;
        db.Reports.Update(existingReport);
        await db.SaveChangesAsync();

        return Utils.Response("", "", 0, HttpStatusCode.OK);
    }

    public static async Task<IResult> DeleteReport(
        [FromBody] Report report,
        User user,
        TwitterCloneContext db
    )
    {
        var existingReport = await db.Reports.FindAsync(report.Id);
        if (existingReport == null)
        {
            return Utils.Response("Report does not exist", "", 0, HttpStatusCode.NotFound);
        }

        db.Reports.Remove(existingReport);
        await db.SaveChangesAsync();

        return Utils.Response("", "", 0, HttpStatusCode.OK);
    }
}
