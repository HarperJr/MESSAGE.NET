using Messanger.Data.Models;
using Messanger.Database;
using Messanger.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Messanger.Http {
   
    public class MultimediaHandler : IHttpHandler {

        private readonly IMultimediaRepository _multimediaRepository;

        public MultimediaHandler() {
            LocalDbContext localDbContext = new LocalDbContext();
            _multimediaRepository = new MultimediaRepository(localDbContext);
        }

        public void ProcessRequest(HttpContext context) {
            var request = context.Request;
            if (request.RequestType.Equals("POST")) {
                if (request.ContentLength > 5242880) {
                    return;
                }
                Image image;
                using (var stream = request.GetBufferedInputStream()) {
                    image = Image.FromStream(stream);
                }
                SaveMultimedia(image, context.Server.MapPath("~/multimedia/"));
            } else {
                MultimediaResponse multimediaResponse = LoadMultimediaFromDir(
                    request.QueryString["multimedia"], context.Server.MapPath("~/multimedia/"));
                if (multimediaResponse != null) {
                    context.Response.Headers.Set("Content-Type", "application/x-www-form-urlencoded");
                    context.Response.Write(new JObject {
                        { "multimediaId", multimediaResponse.Id },
                        { "width", multimediaResponse.Width },
                        { "height", multimediaResponse.Height },
                        { "buffer", multimediaResponse.Buffer }
                    });
                }
            }
        }

        private MultimediaResponse LoadMultimediaFromDir(string id, string dir) {
            Multimedia multimedia = _multimediaRepository
                .GetById(id);
            if (multimedia != null) {
                string path = Path.Combine(dir, multimedia.RemotePath);
                byte[] buffer = File.ReadAllBytes(path);
                return new MultimediaResponse {
                    Id = multimedia.Id,
                    Width = multimedia.Width,
                    Height = multimedia.Height,
                    Buffer = buffer
                };
            }
            return null;
        }

        private void SaveMultimedia(Image image, string path) {
            if (image == null) {
                return;
            }
            string guid = Guid.NewGuid().ToString();
            string remotePath = Guid.NewGuid().ToString();

            if (File.Exists(path)) {
                File.Delete(path);
            }

            image.Save(path);
            _multimediaRepository
                .Insert(new Multimedia {
                    Id = guid,
                    Width = image.Width,
                    Height = image.Height,
                    RemotePath = remotePath
                });
        }

        public bool IsReusable {
            get {
                return true;
            }
        }

        private class MultimediaResponse {

            public string Id { get; set; }

            public int Width { get; set; }

            public int Height { get; set; }

            public byte[] Buffer { get; set; }
        }
    }
}