using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iso18013LibV2.Category
{
    public class DrivingCategory
    {
            /**
     * Publicly accessable map of valid categories, indexed by their string
     * representation.
     */
    public static Dictionary<string, DrivingCategory> categories = null;

    /** The special category to put additional restraints on all categories. */
    public static string ALL = "ALL";

    private string category;

    private string description;

    /**
     * A private constructor that initialises the object and puts it into the
     * global map defined above.
     * 
     * @param category
     *            the string with the actual category (e.g. "A")
     * @param description
     *            the description of this category (e.g. "Motorcycles")
     */
    private DrivingCategory(String category, String description) {
        this.category = category;
        this.description = description;
        categories.Add(category, this);
    }

    /**
     * 
     * @return the category code embedded in this object (A, B, etc.)
     */
    public String getCategory() {
        return category;
    }

    /**
     * 
     * @return the desciription of this category
     */
    public String getDescription() {
        return description;
    }

    /**
     * 
     * @return true if the category is "ALL"
     */
    public bool isNotSpecific() {
        return ALL.Equals(category);
    }

    /**
     * @return string representation of this object
     */
    public String toString() {
        return category;
    }

    /**
     * @return the result of the comparison of two category objects
     */
    public bool equals(Object o) {
        if (o is DrivingCategory) {
            return ((DrivingCategory)o).category.Equals(this.category);
        } else {
            return false;
        }
    }
    }
}
