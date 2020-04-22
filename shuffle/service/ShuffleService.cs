using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using shuffle.models;

namespace shuffle
{
    public class ShuffleService
    {
        public List<Player> Shuffle(List<Attendee> attendees, GameRule rule)
        {
            var shuffledAttendees = Shuffle(attendees).ToList();
            if (string.IsNullOrEmpty(rule.JudgeEmail))
            {
                return GetPlayers(rule, shuffledAttendees).ToList();
            }

            var judge = shuffledAttendees.Single(a => a.Email == rule.JudgeEmail);
            var judgePlayer = new Player(judge, Role.Judge);
            rule.Role2Count.Remove(Role.Judge);
            return GetPlayers(rule, shuffledAttendees)
                .Concat(new List<Player> {judgePlayer}).ToList();
        }

      

        private static IEnumerable<Player> GetPlayers(GameRule rule, List<Attendee> shuffledAttendees)
        {
            var roles = rule.Role2Count.SelectMany(role2count =>
                Enumerable.Repeat(role2count.Key, role2count.Value));

            return shuffledAttendees.Zip(roles, (attendee, role) => new Player(attendee, role));
        }

        private static IList<T> Shuffle<T>(IList<T> list)
        {
            list = list.Select(x => x).ToList();
            var provider = new RNGCryptoServiceProvider();
            var n = list.Count;
            while (n > 1)
            {
                var box = new byte[1];
                do
                {
                    provider.GetBytes(box);
                } while (!(box[0] < n * (byte.MaxValue / n)));

                var k = box[0] % n;
                n--;
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}