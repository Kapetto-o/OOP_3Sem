using System;

public interface GenericInterface<T>
{
    void Add(T item);
    void Remove(T item);
    void ViewAll();
    T FindByPredicate(Predicate<T> predicate);
}