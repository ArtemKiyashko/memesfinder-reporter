using System;
namespace MemesFinderReporter.Models.Reports
{
	public record Report(Uri PictureUri, string Text, long ChatId, int? ThreadId);
}

