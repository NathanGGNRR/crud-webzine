using System;
using System.Collections.Generic;

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    public class IndexCommentairesViewModel
    {
        public List<CommentairesViewModel> Commentaires { get; set; }
        public int TotalCommentaires { get; set; }
        public int PageActuel { get; set; }
        public int LengthPage { get; set; }
        public int DisplayPage { get; set; }
        public Boolean Next { get; set; }

    }
}
