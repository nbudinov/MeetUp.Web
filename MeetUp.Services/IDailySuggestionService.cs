using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.Services
{
    public interface IDailySuggestionService
    {
        void SaveDailySuggestionLog(int userId);

        bool ShouldPopupToday(int userId);

    }
}
