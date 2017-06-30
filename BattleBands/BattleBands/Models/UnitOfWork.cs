using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battle_Bands.Models
{
    public class UnitOfWork
    {
        private UserRepository userRepository;
        private BandRepository bandRepository;
        private EventRepository eventRepository;
        private PhotoRepository photoRepository;
        private VideoRepository videoRepository;
        private MusicRepository musicRepository;

        public UnitOfWork()
        {
            userRepository = new UserRepository();
            bandRepository = new BandRepository();
            eventRepository = new EventRepository();
            photoRepository = new PhotoRepository();
            videoRepository = new VideoRepository();
            musicRepository = new MusicRepository();
    }
        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository();
                return userRepository;
            }
        }
        public BandRepository Bands
        {
            get
            {
                if (bandRepository == null)
                    bandRepository = new BandRepository();
                return bandRepository;
            }
        }
        public EventRepository Events
        {
            get
            {
                if (eventRepository == null)
                    eventRepository = new EventRepository();
                return eventRepository;
            }
        }
        public PhotoRepository Photos
        {
            get
            {
                if (photoRepository == null)
                    photoRepository = new PhotoRepository();
                return photoRepository;
            }
        }
        public VideoRepository Videos
        {
            get
            {
                if (videoRepository == null)
                    videoRepository = new VideoRepository();
                return videoRepository;
            }
        }
        public MusicRepository Musics
        {
            get
            {
                if (musicRepository == null)
                    musicRepository = new MusicRepository();
                return musicRepository;
            }
        }
    }
}
