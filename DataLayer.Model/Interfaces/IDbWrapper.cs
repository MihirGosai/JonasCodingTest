using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
	/// <summary>
	/// The wrapper for all database functions.
	/// </summary>
	/// <typeparam name="T">the database object model inherited from DataObject</typeparam>
	public interface IDbWrapper<T>
	{
		/// <summary>
		/// Insert the object into the database.
		/// </summary>
		/// <param name="data">object to insert</param>
		/// <returns>true on success</returns>
		bool Insert(T data);

		/// <summary>
		/// Update an object in the database.
		/// </summary>
		/// <param name="data">object to update</param>
		/// <returns>true on success</returns>
		bool Update(T data);

		/// <summary>
		/// Find a list of objects based on a linq query.
		/// </summary>
		/// <param name="expression">a linq query for getting objects</param>
		/// <returns>a list of objects on success</returns>
		IEnumerable<T> Find(Expression<Func<T, bool>> expression);

		/// <summary>
		/// Get a list of all objects of the provided type.
		/// </summary>
		/// <returns>a lst of objects on success</returns>
		IEnumerable<T> FindAll();

		/// <summary>
		/// Delete objects based on a linq query.
		/// </summary>
		/// <param name="expression">a linq query for deleting objects</param>
		/// <returns>true on success</returns>
		bool Delete(Expression<Func<T, bool>> expression);

		/// <summary>
		/// Delete all objects of the associated type.
		/// </summary>
		/// <returns>true on success</returns>
		bool DeleteAll();

		/// <summary>
		/// Update all objects by filter query.
		/// </summary>
		/// <param name="filter">Filter for update</param>
		/// <param name="fieldToUpdate">field to update</param>
		/// <param name="newValue">new value</param>
		/// <returns>true on success</returns>
		bool UpdateAll(Expression<Func<T, bool>> filter, string fieldToUpdate, object newValue);


		/// <summary>
		/// Insert the object into the database.
		/// </summary>
		/// <param name="data">object to insert</param>
		/// <returns>task for true on success</returns>
		Task<bool> InsertAsync(T data);

		/// <summary>
		/// Update an object in the database.
		/// </summary>
		/// <param name="data">object to update</param>
		/// <returns>task for true on success</returns>
		Task<bool> UpdateAsync(T data);

		/// <summary>
		/// Find a list of objects based on a linq query.
		/// </summary>
		/// <param name="expression">a linq query for getting objects</param>
		/// <returns>task for a lst of objects on success</returns>
		Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

		/// <summary>
		/// Get a list of all objects of the provided type.
		/// </summary>
		/// <returns>task for a lst of objects on success</returns>
		Task<IEnumerable<T>> FindAllAsync();

		/// <summary>
		/// Delete objects based on a linq query.
		/// </summary>
		/// <param name="expression">a linq query for deleting objects</param>
		/// <returns>task for true on success</returns>
		Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);

		/// <summary>
		/// Delete all objects of the associated type.
		/// </summary>
		/// <returns>task for true on success</returns>
		Task<bool> DeleteAllAsync();

		/// <summary>
		/// Update all objects by filter query.
		/// </summary>
		/// <param name="filter">Filter for update</param>
		/// <param name="fieldToUpdate">field to update</param>
		/// <param name="newValue">new value</param>
		/// <returns>task for true on success</returns>
		Task<bool> UpdateAllAsync(Expression<Func<T, bool>> filter, string fieldToUpdate, object newValue);

	}
}
