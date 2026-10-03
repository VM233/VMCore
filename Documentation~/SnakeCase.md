# Snake case normalization

`WorldsUtility.ToSnakeCase(string)` owns normalization. The input and result are
immutable strings; callers consume the result directly. There is no registration,
cache, invalidation, global state, or teardown. `SimplePropertyProvider` in
MarbleBattlers consumes this utility while constructing persistence keys in Awake.

The string overload recognizes exactly `[a-z]+(_[a-z]+)*` and returns that input
without constructing words. For this domain, GetWords emits each lowercase word
unchanged, current-culture ToLower preserves every ASCII lowercase letter, and
joining with underscores reconstructs the input. Returning the immutable input
therefore preserves the complete value contract. Reference identity is not an
ownership or mutation contract for a string.

All other inputs retain the existing GetWords, current-culture lowercase, and
joining semantics. This includes null, empty and whitespace inputs, leading or
trailing separators, repeated separators, digits, acronyms, capitals, accents,
CJK text and other Unicode. The IEnumerable overload remains unchanged. The
recognizer is an exact semantic partition, not a replacement parser or cache.

## Static Cost Ledger

For an input of N UTF-16 code units, recognition examines at most N characters,
allocates zero bytes and calls no external owner. Its retained state is one
Boolean and loop state. Noncanonical input additionally takes the existing word
pipeline; its public behavior and allocation ownership do not change. There are
no new nested input axes, deferred work, main-thread scheduling or publication.
The canonical input budget is one linear scan and zero allocations: PASS.

The focused Editor tests have two exhaustive cultures and a seven-character
alphabet at lengths 0 through 4: 1+7+49+343+2401 = 2801 inputs per culture.
Two normalizations per input give at most 11204 normalization calls and 44816
input character visits per normalization stage. Inputs are generated and released
individually, with no retained corpus. Six cultures also cover a fixed 35-input
corpus. The allocation fixture performs 64 initialization iterations per path
and then 4096 calls per path on one 16-character key. A known positive control
creates sixteen 1024-byte arrays. Three sequential thread-local GC.Alloc recorder
windows retain one summed sample each, stop before assertions and dispose before
the next window. Only one native recorder is live at a time. A separate
8192-character canonical input proves long-input identity. The fixture owns at
most 32 KiB of live input strings, 16 KiB of positive-control arrays and a 64 MiB
temporary normalization allocation budget. There are no scenes, gameplay or
retained Profiler frame captures: PASS.

## Verification and scope

Run `VMCore.Editor.Tests`, filtered to `VMFramework.Tests.SnakeCaseTests`, through
the existing native package-test workflow of the consuming project. The tests
compare the string overload to the separately exposed word/sequence pipeline,
prove exact canonical reference identity, and measure GC.Alloc events on the
calling thread. The known array allocation must register events before zero can
be interpreted; the word pipeline must also register allocations. Unity's
ProfilerRecorder API owns this observation, including on Editors whose managed
GC byte counter reports zero. Both paths use the same native measurement contract.
See the official [thread-local recorder](https://docs.unity.cn/2022.3/Documentation/ScriptReference/Unity.Profiling.ProfilerRecorderOptions.CollectOnlyOnCurrentThread.html)
and [summed sample](https://docs.unity.com/en-us/engine/6000.6/script-reference/unity/profiling/profilerrecorderoptions/sumallsamplesinframe)
documentation. Supported Unity versions remain declared by package metadata and
the README.

This repair removes a source-proven allocation contributor. It does not establish
that a game-specific cold creation or physics hitch has been fixed. Those require
the original gameplay requests and current-source performance evidence.
