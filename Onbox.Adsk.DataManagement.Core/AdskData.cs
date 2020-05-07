using System;
using System.Collections.Generic;

namespace Onbox.Adsk.DataManagement.Core
{
    public partial class AdskData
    {
        public Hubs Hubs { get; set; }
        public Hub Hub { get; set; }
        public Projects Projects { get; set; }
        public Project Project { get; set; }
        public TopFolders TopFolders { get; set; }
        public Download Download { get; set; }
        public Job Job { get; set; }
        public Object Object { get; set; }
        public Folder Folder { get; set; }
        public FolderContents FolderContents { get; set; }
        public Item Item { get; set; }
        public Version Version { get; set; }
    }

    public partial class Download
    {
        public Jsonapi Jsonapi { get; set; }
        public DataLinks Links { get; set; }
        public DownloadData Data { get; set; }
    }

    public partial class DownloadData
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public DownloadAttributes Attributes { get; set; }
        public DownloadRelationships Relationships { get; set; }
        public DataLinks Links { get; set; }
    }

    public partial class DownloadAttributes
    {
        public Format Format { get; set; }
    }

    public partial class Format
    {
        public string FileType { get; set; }
    }

    public partial class DataLinks
    {
        public Schema Self { get; set; }
    }

    public partial class Schema
    {
        public string Href { get; set; }
    }

    public partial class DownloadRelationships
    {
        public Source Source { get; set; }
        public Storage Storage { get; set; }
    }

    public partial class Source
    {
        public SourceLinks Links { get; set; }
        public SourceData Data { get; set; }
    }

    public partial class SourceData
    {
        public string Type { get; set; }
        public string Id { get; set; }
    }

    public partial class SourceLinks
    {
        public Schema Related { get; set; }
    }

    public partial class Storage
    {
        public SourceData Data { get; set; }
        public Meta Meta { get; set; }
    }

    public partial class Meta
    {
        public Schema Link { get; set; }
    }

    public partial class Jsonapi
    {
        public string Version { get; set; }
    }

    public partial class Folder
    {
        public Jsonapi Jsonapi { get; set; }
        public DataLinks Links { get; set; }
        public FolderData Data { get; set; }
    }

    public partial class FolderData
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public FolderAttributes Attributes { get; set; }
        public DataLinks Links { get; set; }
        public FolderRelationships Relationships { get; set; }
    }

    public partial class FolderAttributes
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public string CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTimeOffset LastModifiedTime { get; set; }
        public string LastModifiedUserId { get; set; }
        public string LastModifiedUserName { get; set; }
        public long ObjectCount { get; set; }
        public bool Hidden { get; set; }
        public FolderExtension Extension { get; set; }
    }

    public partial class FolderExtension
    {
        public string Type { get; set; }
        public string Version { get; set; }
        public Schema Schema { get; set; }
        public FolderExtensionData Data { get; set; }
    }

    public partial class FolderExtensionData
    {
        public List<string> AllowedTypes { get; set; }
        public List<string> VisibleTypes { get; set; }
    }

    public partial class FolderRelationships
    {
        public Source Parent { get; set; }
        public Refs Refs { get; set; }
        public RelationshipsLinks Links { get; set; }
        public Contents Contents { get; set; }
    }

    public partial class Contents
    {
        public SourceLinks Links { get; set; }
    }

    public partial class RelationshipsLinks
    {
        public DataLinks Links { get; set; }
    }

    public partial class Refs
    {
        public RefsLinks Links { get; set; }
    }

    public partial class RefsLinks
    {
        public Schema Self { get; set; }
        public Schema Related { get; set; }
    }

    public partial class FolderContents
    {
        public Jsonapi Jsonapi { get; set; }
        public Links Links { get; set; }
        public List<ItemBase> Data { get; set; }
        public List<Included> Included { get; set; }
    }

    public partial class ItemBase
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public ItemBaseAttributes Attributes { get; set; }
        public DataLinks Links { get; set; }
        public ItemRelationships Relationships { get; set; }
    }

    public partial class ItemBaseAttributes
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public string CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTimeOffset LastModifiedTime { get; set; }
        public string LastModifiedUserId { get; set; }
        public string LastModifiedUserName { get; set; }
        public string Path { get; set; }
        public long? ObjectCount { get; set; }
        public bool Hidden { get; set; }
        public FolderExtension Extension { get; set; }
        public bool? Reserved { get; set; }
        public DateTimeOffset? ReservedTime { get; set; }
        public string ReservedUserId { get; set; }
        public string ReservedUserName { get; set; }
    }

    public partial class ItemRelationships
    {
        public Source Parent { get; set; }
        public Refs Refs { get; set; }
        public RelationshipsLinks Links { get; set; }
        public Contents Contents { get; set; }
        public Source Tip { get; set; }
        public Contents Versions { get; set; }
    }

    public partial class Included
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public VersionAttributes Attributes { get; set; }
        public DataLinks Links { get; set; }
        public IncludedRelationships Relationships { get; set; }
    }

    public partial class VersionAttributes
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public string CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTimeOffset LastModifiedTime { get; set; }
        public string LastModifiedUserId { get; set; }
        public string LastModifiedUserName { get; set; }
        public long VersionNumber { get; set; }
        public string MimeType { get; set; }
        public IncludedExtension Extension { get; set; }
    }

    public partial class IncludedExtension
    {
        public string Type { get; set; }
        public string Version { get; set; }
        public Schema Schema { get; set; }
        public IncludedData Data { get; set; }
    }

    public partial class IncludedData
    {
        public object TempUrn { get; set; }
        public Data Properties { get; set; }
        public string StorageUrn { get; set; }
        public string StorageType { get; set; }
    }

    public partial class Data
    {
    }

    public partial class IncludedRelationships
    {
        public Source Item { get; set; }
        public Refs Refs { get; set; }
        public RelationshipsLinks Links { get; set; }
        public Storage Storage { get; set; }
    }

    public partial class Links
    {
        public Schema Self { get; set; }
        public Schema First { get; set; }
        public Schema Prev { get; set; }
        public Schema Next { get; set; }
    }

    public partial class Hub
    {
        public Jsonapi Jsonapi { get; set; }
        public DataLinks Links { get; set; }
        public HubData Data { get; set; }
    }

    public partial class HubData
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public HubAttributes Attributes { get; set; }
        public HubRelationships Relationships { get; set; }
        public DataLinks Links { get; set; }
    }

    public partial class HubAttributes
    {
        public string Name { get; set; }
        public AttributesExtension Extension { get; set; }
        public string Region { get; set; }
    }

    public partial class AttributesExtension
    {
        public Data Data { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }
        public Schema Schema { get; set; }
    }

    public partial class HubRelationships
    {
        public Contents Projects { get; set; }
    }

    public partial class Hubs
    {
        public Jsonapi Jsonapi { get; set; }
        public DataLinks Links { get; set; }
        public List<HubData> Data { get; set; }
    }

    public partial class Item
    {
        public Jsonapi Jsonapi { get; set; }
        public DataLinks Links { get; set; }
        public ItemData Data { get; set; }
        public List<Included> Included { get; set; }
    }

    public partial class ItemData
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public ItemAttributes Attributes { get; set; }
        public ItemRelationships Relationships { get; set; }
        public DataLinks Links { get; set; }
    }

    public partial class ItemAttributes
    {
        public string DisplayName { get; set; }
        public AttributesExtension Extension { get; set; }
        public string CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public string LastModifiedUserId { get; set; }
        public string LastModifiedUserName { get; set; }
        public DateTimeOffset LastModifiedTime { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public bool Hidden { get; set; }
        public bool Reserved { get; set; }
        public DateTimeOffset ReservedTime { get; set; }
        public string ReservedUserId { get; set; }
        public string ReservedUserName { get; set; }
    }

    public partial class Job
    {
        public Jsonapi Jsonapi { get; set; }
        public DataLinks Links { get; set; }
        public JobData Data { get; set; }
    }

    public partial class JobData
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public JobAttributes Attributes { get; set; }
        public DataLinks Links { get; set; }
    }

    public partial class JobAttributes
    {
        public string Status { get; set; }
    }

    public partial class Object
    {
        public Jsonapi Jsonapi { get; set; }
        public DataLinks Links { get; set; }
        public ObjectData Data { get; set; }
    }

    public partial class ObjectData
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public ObjectRelationships Relationships { get; set; }
    }

    public class ObjectReference
    {
        public string Bucketkey { get; set; }

        public string ObjectId { get; set; }

        public string ObjectKey { get; set; }

        public string Sha1 { get; set; }

        public long Size { get; set; }

        public string ContentType { get; set; }

        public string Location { get; set; }
    }

    public partial class ObjectRelationships
    {
        public Source Target { get; set; }
    }

    public partial class Project
    {
        public Jsonapi Jsonapi { get; set; }
        public DataLinks Links { get; set; }
        public ProjectData Data { get; set; }
    }

    public partial class ProjectData
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public ProjectAttributes Attributes { get; set; }
        public ProjectRelationships Relationships { get; set; }
        public DataLinks Links { get; set; }
    }

    public partial class ProjectAttributes
    {
        public string Name { get; set; }
        public List<string> Scopes { get; set; }
        public AttributesExtension Extension { get; set; }
    }

    public partial class ProjectRelationships
    {
        public Source Hub { get; set; }
        public Storage RootFolder { get; set; }
        public Contents TopFolders { get; set; }
    }

    public partial class Projects
    {
        public Jsonapi Jsonapi { get; set; }
        public Links Links { get; set; }
        public List<ProjectData> Data { get; set; }
    }

    public partial class TopFolders
    {
        public Jsonapi Jsonapi { get; set; }
        public DataLinks Links { get; set; }
        public List<FolderData> Data { get; set; }
    }

    public partial class Version
    {
        public Jsonapi Jsonapi { get; set; }
        public DataLinks Links { get; set; }
        public VersionData Data { get; set; }
    }

    public partial class VersionData
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public VersionAttributes Attributes { get; set; }
        public DataLinks Links { get; set; }
        public VersionRelationships Relationships { get; set; }
    }

    public partial class SearchResults
    {
        public Jsonapi Jsonapi { get; set; }
        public List<VersionData> Data { get; set; }
    }

    public partial class VersionRelationships
    {
        public Source Item { get; set; }
        public Refs Refs { get; set; }
        public RelationshipsLinks Links { get; set; }
        public Storage Storage { get; set; }
        public Storage Derivatives { get; set; }
        public Storage Thumbnails { get; set; }
        public Contents DownloadFormats { get; set; }
    }

}
