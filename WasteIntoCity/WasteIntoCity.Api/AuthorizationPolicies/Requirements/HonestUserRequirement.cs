using Microsoft.AspNetCore.Authorization;

namespace WasteIntoCity.Api.AuthorizationPolicies.Requirements
{
    public class HonestUserRequirement : IAuthorizationRequirement
    {
        public HonestUserRequirement(int rankingMin)
        {
            RankingMin = rankingMin;
        }

        public int RankingMin { get; }
    }
}
