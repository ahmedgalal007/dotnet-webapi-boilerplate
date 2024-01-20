using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Storage;
public class File : AuditableEntity
{
    public Guid FolderId { get; set; }
    // public Folder Folder { get; set; }
    public bool IsPublic { get; set; } = false;
    public string Name { get; set; }
    public string Extention { get; set; }
}

// ----------------   ImageManager File Response ----------------------
// {
//     action: "rename",
//     data: [
//         {
//             dateCreated: "2019-03-20T05:22:34.621Z",
//             dateModified: "2019-03-20T08:45:56.000Z",
//             filterPath: "\Pictures\Nature\",
//             hasChild: false,
//             iconClass: "e-fe-image",
//             isFile: true,
//             name: "seaviews.jpg",
//             size: 95866,
//             type: ".jpg"
//         }
//     ],
//     newname: "seaview.jpg",
//     name: "seaviews.jpg",
//     path: "/Pictures/Nature/"
// }
// ----------------------------------------
// {
//     cwd:null,
//     files:[
//         {
//             name:"seaview.jpg",
//             size:95866,
//             dateModified:"2019-03-20T08:45:56+00:00",
//             dateCreated:"2019-03-20T05:22:34.6214847+00:00",
//             hasChild:false,
//             isFile:true,
//             type:".jpg",
//             filterPath:"\\Pictures\\Nature\\seaview.jpg"
//         }
//     ],
//     error: null,
//     details: null
// }

// -------------------------------------------------------------

// {
//     action: "delete",
//     path: "/Hello/",
//     names: ["New"],
//     data: []
// }
// -----------------------------------------------------------
// {
//     cwd: null,
//     details: null,
//     error: null,
//     files: [
//         {
//             dateCreated: "2019-03-15T10:13:30.346309+00:00",
//             dateModified: "2019-03-15T10:13:30.346309+00:00",
//             filterPath: "\Hello\folder",
//             hasChild: true,
//             isFile: false,
//             name: "folder",
//             size: 0,
//             type: ""
//         }
//     ]
// }

// ---------------------------------------------------------
// {
//     action: "details",
//     path: "/FileContents/",
//     names: ["All Files"],
//     data: []
// }

// ---------------------------------------------------------

// {
//     cwd:null,
//     files:null,
//     error:null,
//     details:
//     {
//         name:"All Files",
//         location:"\\Files\\FileContents\\All Files",
//         isFile:false,
//         size:"679.8 KB",
//         created:"3/8/2019 10:18:37 AM",
//         modified:"3/8/2019 10:18:39 AM",
//         multipleFiles:false
//     }
// }

// -------------------------------------------------------------

// {
//     action: "search",
//     path: "/",
//     searchString: "*nature*",
//     showHiddenItems: false,
//     caseSensitive: false,
//     data: []
// }

// {
//     cwd:
//     {
//         name:files,
//         size:0,
//         dateModified:"2019-03-15T10:07:00.8658158+00:00",
//         dateCreated:"2019-02-27T17:36:15.6571949+00:00",
//         hasChild:true,
//         isFile:false,
//         type:"",
//         filterPath:"\\"
//     },
//     files:
//     [
//         {
//             name: "Nature",
//             size: 0,
//             dateModified: "2019-03-08T10:18:42.9937708+00:00",
//             dateCreated: "2019-03-08T10:18:42.5907729+00:00",
//             hasChild: true,
//             isFile: false,
//             type: "",
//             filterPath: "\\FileContents\\Nature"
//         }
//     ],
//     error: null,
//     details: null
// }

// ---------------------------------------------------------------------

// {
//     action: "copy",
//     path: "/",
//     names: ["6.png"],
//     renameFiles: ["6.png"],
//     targetPath: "/Videos/"
// }

// {
//     cwd:null,
//     files:[
//         {
//             path:null,
//             action:null,
//             newName:null,
//             names:null,
//             name:"justin.mp4",
//             size:0,
//             previousName:"album.mp4",
//             dateModified:"2019-06-21T06:58:32+00:00",
//             dateCreated:"2019-06-24T04:22:14.6245618+00:00",
//             hasChild:false,
//             isFile:true,
//             type:".mp4",
//             id:null,
//             filterPath:"\\"
//         }
//     ],
//     error: null,
//     details: null
// }

// -------------------------------------------------------------

// {
//     action: "move",
//     path: "/",
//     names: ["6.png"],
//     renameFiles: ["6.png"],
//     targetPath: "/Videos/"
// }

// {
//     cwd:null,
//     files:[
//         {
//             path:null,
//             action:null,
//             newName:null,
//             names:null,
//             name:"justin biber.mp4",
//             size:0,
//             previousName:"justin biber.mp4",
//             dateModified:"2019-06-21T06:58:32+00:00",
//             dateCreated:"2019-06-24T04:26:49.2690476+00:00",
//             hasChild:false,
//             isFile:true,
//             type:".mp4",
//             id:null,
//             filterPath:"\\Videos\\"
//         }
//     ],
//     error: null,
//     details: null
// }