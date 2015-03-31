using RED.Models.RepositoryBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RED.Models.ElectronicDiary.Requests
{
    public class RequestsRepository : RepositoryBase
    {
        public RequestW GetRequst(Guid id)
        {
            var request = db.Requests.Single(r => r.Id == id);
            return new RequestW(request);
        }

        //might change after we discuss how the requests behave
        public IEnumerable<RequestW> GetRequests()
        {
            var requests = db.Requests.ToList();
            return requests.Select(r => new RequestW(r));
        }


    }
}