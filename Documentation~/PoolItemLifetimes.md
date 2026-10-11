# Pool item lifecycle

The creator owns a factory product after its factory returns. Failed fresh
OnCreate retires that acquired prefix with OnClear; a factory which throws before
returning a product must retire its own unpublished construction resources.
Failed pooled OnGet returns only the actual dequeued product. A caller receives
no rental when either prefix fails. Successful cleanup rethrows the original
initialization exception. Independent cleanup failures preserve original object
identity and ordering in PoolItemAcquisitionException, with actual Pool, Item and Phase.

## Lease retirement and retained storage

Return invokes OnReturn once, then the supplied actual retirement publisher,
before making this object available to another Get. The publisher is an optional
owner capability on the same Return producer, not a second pool or transaction.
Each independent phase is attempted. After the publisher returns, current queue
capacity decides retention. A full queue or any retirement/publication failure
retires physical storage with OnClear, and does not enqueue the failed product.
This preserves the configured capacity under legal callback Get/Return reentry.

OnReturn ends the active lease. OnClear releases its physical storage, including
the prefix of a failed fresh acquisition. An implementation whose Clear also ends
a still-active lease must use its actual existing lifecycle fact to avoid repeating
OnReturn when storage belongs to a lease already ended. PoolItemBehaviour already
implements separate Return (deactivation/reparenting) and Clear (native Destroy).
No successful cleanup or IsDestroyed value asserts native absence.

Clear snapshots its entry-time held queue and withdraws it before callbacks.
Every member in that finite snapshot is cleared once. Failures retain actual
member identities and original exceptions; later members still run. Legal new
returns during these callbacks belong to the new queue and are not recursively
drained. Snapshot allocation failure occurs before ownership withdrawal.

Return is O(1) plus the actual supplied callback; Clear is O(Q) in entry queue
size and retains Q local references. Neither operation is thread-safe. Callbacks
do not acquire authority to return the same borrowed object again; a legal new
rental is a distinct lease obtained through Get.

Focused acceptance is defined by VMCore.Editor.Tests / PoolItemAcquisitionTests.
This source contract is independent of Unity's deferred native destruction.
