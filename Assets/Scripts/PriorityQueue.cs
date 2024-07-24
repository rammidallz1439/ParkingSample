using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<T> elements;
    private Comparison<T> comparison;

    public PriorityQueue(Comparison<T> comparison)
    {
        elements = new List<T>();
        this.comparison = comparison;
    }

    public int Count => elements.Count;

    public void Enqueue(T item)
    {
        elements.Add(item);
        int ci = elements.Count - 1;
        while (ci > 0)
        {
            int pi = (ci - 1) / 2;
            if (comparison(elements[ci], elements[pi]) >= 0) break;
            T tmp = elements[ci];
            elements[ci] = elements[pi];
            elements[pi] = tmp;
            ci = pi;
        }
    }

    public T Dequeue()
    {
        int li = elements.Count - 1;
        T frontItem = elements[0];
        elements[0] = elements[li];
        elements.RemoveAt(li);

        --li;
        int pi = 0;
        while (true)
        {
            int ci = pi * 2 + 1;
            if (ci > li) break;
            int rc = ci + 1;
            if (rc <= li && comparison(elements[rc], elements[ci]) < 0)
                ci = rc;
            if (comparison(elements[pi], elements[ci]) <= 0) break;
            T tmp = elements[pi];
            elements[pi] = elements[ci];
            elements[ci] = tmp;
            pi = ci;
        }
        return frontItem;
    }

    public bool Contains(T item)
    {
        return elements.Contains(item);
    }
}
