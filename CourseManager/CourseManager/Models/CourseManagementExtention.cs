using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseManager.Models
{
    public partial class CourseManagements
    {
        [Display(Name = "班主任")]
        public string TeacherName 
        {
            get {
                CourseManagerEntities db = new CourseManagerEntities();
                var teacher = db.Teachers.Where(t => t.Id == TeacherId).FirstOrDefault();
                if (teacher == null) {
                    return "";
                }
                return teacher.Name;
            }
        
        }

        [Display(Name = "班级")]
        public string ClassName
        {
            get
            {

                CourseManagerEntities db = new CourseManagerEntities();
                var class_ = db.Classes.Where(t => t.Id == ClassId).FirstOrDefault();
                if (class_ == null)
                {
                    return "";
                }
                return class_.Name;
            }

        }

        [Display(Name = "课程")]
        public string CourseName
        {
            get
            {

                CourseManagerEntities db = new CourseManagerEntities();
                var course = db.Courses.Where(t => t.Id == CourseId).FirstOrDefault();
                if (course == null)
                {
                    return "";
                }
                return course.Name;
            }

        }
    }
}