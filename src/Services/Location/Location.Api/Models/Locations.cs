﻿using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace MyBlog.Location.Api.Models
{
    public class Locations
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public int LocationId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        
        public Point Location { get; set; }

        public Polygon Polygon { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ParentId { get; set; }

        public void SetLocation(double longitude, double latitude)
        {
            Location = new Point(longitude, latitude);
        }

        public void SetArea(IEnumerable<GeoJson2DGeographicCoordinates> coordinates)
        {
            Polygon = new Polygon(coordinates);
        }
    }
}
