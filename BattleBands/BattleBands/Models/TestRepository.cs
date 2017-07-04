using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands.Models
{
    public class UserRepository : IRepository<User>
    {
        private List<User> Users;
        private User User;
        public void Add(User item)
        {
            Users.Add(item);
        }

        //public void Create(UserModel item)
        //{
        //    throw new NotImplementedException();
        //}

        public void Delete(User item)
        {
            Users.Remove(Users.Find(
                delegate (User um)
                {
                    if (um.Username == item.Username && um.Password == item.Password)
                    {
                        return true;
                    }
                    else return false;
                })
                );
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        public User GetItem(Guid id)
        {
            User = Users.Find(x => x.IdUser == id);
            return User;
        }

        public IEnumerable<User> GetList()
        {
            return Users;
        }

        public void Update(User item)
        {
           for (int i = 0; i != Users.Count(); i++)
            {
                if (Users[i].Username == User.Username && Users[i].Password == User.Password)
                {
                    Users[i].Username = item.Username;
                    Users[i].Password = item.Password;
                    break;
                }
           }
        }
    }


    public class BandRepository : IRepository<BandModel>
    {
        private List<BandModel> Bands;
        private BandModel Band;

        public void Add(BandModel item)
        {
            Bands.Add(item);
        }

        //public void Create(BandModel item)
        //{
        //    throw new NotImplementedException();
        //}

        public void Delete(BandModel item)
        {
            Bands.Remove(Bands.Find(
                delegate (BandModel um)
                {
                    if (um.BandName == item.BandName)
                    {
                        return true;
                    }
                    else return false;
                })
                );
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        public BandModel GetItem(Guid id)
        {
            Band = Bands.Find(x => x.IdBand == id);
            return Band;
        }

        public IEnumerable<BandModel> GetList()
        {
            return Bands;
        }

        public void Update(BandModel item)
        {
            for (int i = 0; i != Bands.Count(); i++)
            {
                if (Bands[i].BandName == Band.BandName)
                {
                    Bands[i].BandName = item.BandName;
                    break;
                }
            }
        }
    }
    public class MusicRepository : IRepository<MusicModel>
    {
        private MusicModel Music;
        private List<MusicModel> Musics;

        public void Add(MusicModel item)
        {
            Musics.Add(item);
        }

        //public void Create(MusicModel item)
        //{
        //    throw new NotImplementedException();
        //}

        public void Delete(MusicModel item)
        {
            Musics.Remove(Musics.Find(
               delegate (MusicModel mm)
               {
                   if (mm.MusicName == item.MusicName)
                   {
                       return true;
                   }
                   else return false;
               })
               );
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        public MusicModel GetItem(Guid id)
        {
            Music = Musics.Find(x => x.IdMusic == id);
            return Music;
        }

        public IEnumerable<MusicModel> GetList()
        {
            return Musics;
        }

        public void Update(MusicModel item)
        {
            for (int i = 0; i != Musics.Count(); i++)
            {
                if (Musics[i].MusicName == item.MusicName)
                {
                    Musics[i].MusicName = item.MusicName;
                    break;
                }
            }
        }
    }
    public class VideoRepository : IRepository<VideoModel>
    {
        private VideoModel Video;
        private List<VideoModel> Videos;

        public void Add(VideoModel item)
        {
            Videos.Add(item);
        }

        public void Delete(VideoModel item)
        {
            Videos.Remove(Videos.Find(
               delegate (VideoModel vm)
               {
                  
                   if (vm.VideoName == item.VideoName)
                   {
                       return true;
                   }
                   else return false;
               })
               );
        }

        public VideoModel GetItem(Guid id)
        {
            Video = Videos.Find(x => x.IdVideo == id);
            return Video;
        }

        public IEnumerable<VideoModel> GetList()
        {
            return Videos;
        }

        public void Update(VideoModel item)
        {
            for (int i = 0; i != Videos.Count(); i++)
            {
                if (Videos[i].VideoName == item.VideoName)
                {
                    Videos[i].VideoName = item.VideoName;
                    break;
                }
            }
        }
    }
    public class PhotoRepository : IRepository<PhotoModel>
    {
        private PhotoModel Photo;
        private List<PhotoModel> Photos;

        public void Add(PhotoModel item)
        {
            Photos.Add(item);
        }

        public void Delete(PhotoModel item)
        {
            Photos.Remove(Photos.Find(
               delegate (PhotoModel vm)
               {

                   if (vm.PhotoName == item.PhotoName)
                   {
                       return true;
                   }
                   else return false;
               })
               );
        }

        public PhotoModel GetItem(Guid id)
        {
            Photo = Photos.Find(x => x.IdPhoto == id);
            return Photo;
        }

        public IEnumerable<PhotoModel> GetList()
        {
            return Photos;
        }

        public void Update(PhotoModel item)
        {
            for (int i = 0; i != Photos.Count(); i++)
            {
                if (Photos[i].PhotoName == item.PhotoName)
                {
                    Photos[i].PhotoName = item.PhotoName;
                    break;
                }
            }
        }
    }
    public class EventRepository : IRepository<EventModel>
    {
        private EventModel Event;
        private List<EventModel> Events;

        public void Add(EventModel item)
        {
            Events.Add(item);
        }

        public void Delete(EventModel item)
        {
            Events.Remove(Events.Find(
               delegate (EventModel vm)
               {

                   if (vm.EventName == item.EventName)
                   {
                       return true;
                   }
                   else return false;
               })
               );
        }

        public EventModel GetItem(Guid id)
        {
            Event = Events.Find(x => x.IdEvent == id);
            return Event;
        }

        public IEnumerable<EventModel> GetList()
        {
            return Events;
        }

        public void Update(EventModel item)
        {
            for (int i = 0; i != Events.Count(); i++)
            {
                if (Events[i].EventName == item.EventName)
                {
                    Events[i].EventName = item.EventName;
                    break;
                }
            }
        }
    }
}
