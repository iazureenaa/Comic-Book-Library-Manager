using ComicBookShared.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookShared.Data
{
    public class SeriesRepository : BaseRepository<Series>
    {
        public SeriesRepository(Context context)
            : base(context)
        {
        }

        public override IList<Series> GetList()
        {
            return Context.Series
                .OrderBy(s => s.Title)
                .ToList();
        }

        public override Series Get(int id, bool includeRelatedEntities = true)
        {
            var series = Context.Series.AsQueryable();

            if (includeRelatedEntities)
            {
                series = series
                    .Include(s => s.ComicBooks)
                    .OrderBy(s => s.Id);
            }

            return series
                .Where(s => s.Id == id)
                .SingleOrDefault();
        }
    }
}
