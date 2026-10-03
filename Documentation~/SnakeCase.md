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
and then 4096 calls per path on one 16-character key. A separate 8192-character
canonical input proves long-input identity. The fixture owns at most 32 KiB of
live input strings and its temporary normalization allocation budget is 64 MiB;
no gameplay, scene objects or Unity API calls are involved: PASS.

## Verification and scope

Run `VMCore.Editor.Tests`, filtered to `VMFramework.Tests.SnakeCaseTests`, through
the existing native package-test workflow of the consuming project. The tests
compare the string overload to the separately exposed word/sequence pipeline,
prove exact canonical reference identity, and measure allocations on the calling
thread. All Unity APIs used here are unchanged; supported Unity versions continue
to be declared by package metadata and the README.

This repair removes a source-proven allocation contributor. It does not establish
that a game-specific cold creation or physics hitch has been fixed. Those require
the original gameplay requests and current-source performance evidence.
