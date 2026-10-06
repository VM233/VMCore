# Collection pools

`CollectionPool<TCollection>.Default` owns the pool for one thread; `Shared`
owns the pool that can be used across threads. List, dictionary, hash-set,
queue and sorted-dictionary pool helpers expose these same entry points.

The two products have independent CLR type-initialization lifetimes. Accessing
one entry point creates only its requested policy, queue and pool. The two
factory delegates are also initialized independently. Each product
is published once for its closed collection type and remains available for
that application domain. This avoids constructing the concurrent queue and
its generic runtime context when a caller uses only the single-thread pool.
The original shared initializer did that extra work during damage and Buff
callbacks in a captured MarbleBattlers Editor performance witness.

The default pool still retains at most 500 collections. The shared pool keeps
its existing processor-based capacity. Collection factories still clear a
borrowed collection before returning it to the caller. Return, rejection,
retention, clear and thread-safety behavior belong to the existing pool and
policy implementations; this change does not alter those contracts.

No warm-up, timing threshold or fallback is involved. First use of the pool
that is actually requested can still incur runtime initialization or method
compilation. The change removes unrequested work; it does not claim to
eliminate all Editor pauses.

## Static Cost Ledger

Production access reads one scalar field. Each closed pool type and each
closed factory type has at most two CLR holders. A first pool access constructs one existing policy and
one existing pool; requesting both constructs the same two products as before.
There is no loop, scan, timer, background thread, additional collection lease
or alternate pool. Each owner has one writer, the CLR initializer, and one
application-domain lifetime. Existing capacities and collection creation costs
remain unchanged. No additional warm path allocation occurs. PASS.

The focused fixture uses three distinct closed collection types. It performs
at most twenty pool/factory accesses and twenty collection leases/returns, plus
four threads each reading one shared product and factory. All loops have four elements or fewer;
thread joins have a 5-second observation limit. Test storage is four pools,
four threads, four result references in each of four fixed arrays, three
small lists and four additional small lists, below 64 KiB excluding the existing CLR/Unity
test runtime. One initialization race is the only concurrent portion; no
Unity APIs, scene, gameplay simulation, source enumeration or timing assertion
are used. PASS.
