<!--
Sync Impact Report
- Version change: initial project constitution → 1.0.0
- Result: established ten project principles, technical constraints, workflow, and governance
- Confirmed constraints: Visual Studio 2026, GitHub Copilot, C#, Windows Forms, and .NET Framework 4.7.2
- Confirmed workflow: specification, clarification, technical planning, tasks, TDD implementation,
  verification, and Pull Request toward `master`
- Removed: template examples, unresolved markers, duplicate version data, and pending TODOs
-->
# GestorIncidencias Constitution

## Core Principles

### I. Visual Studio and Copilot Development
All development MUST be performed in Visual Studio 2026 with GitHub Copilot. Copilot output
MUST be reviewed by the developer before it is accepted into the codebase.

### II. Established Technology Stack
The application MUST use C#, Windows Forms, and .NET Framework 4.7.2. Changes MUST preserve
compatibility with the existing project target unless a separately approved specification
changes that constraint.

### III. Specifications Are the Source of Truth
Specifications MUST be the reference for planning, code generation, and verification. Work MUST
begin by creating or updating the relevant specification before implementation changes are made.

### IV. Reviewed Task-Driven Generation
GitHub Copilot MUST generate and correct code only from reviewed tasks derived from the current
specifications. Unspecified behavior MUST NOT be introduced silently; it requires an explicit
specification update.

### V. Simple Separation of Responsibilities
The solution MUST use a simple organization with Entidades, Controlador, and Formularios.
Business rules MUST remain separate from the user interface, and each new responsibility MUST
be placed in the simplest suitable layer.

### VI. Designer-Compatible Forms
Every Windows Form MUST remain functional in the Visual Studio designer and MUST display its
controls correctly. Designer-generated code MUST be preserved as designer-managed code and
business logic MUST NOT be embedded in a way that prevents design-time loading.

### VII. Clear Input Validation
All user inputs MUST be validated at the appropriate boundary. Validation failures MUST be
communicated clearly to the user and MUST prevent invalid data from reaching business rules.

### VIII. Automated Tests and TDD
Business rules MUST have automated tests. New or changed business behavior MUST demonstrate a
Red-Green-Refactor cycle: a failing test, the implementation that makes it pass, and a reviewed
refactoring step where applicable.

### IX. Short-Lived Branches and Reviewed Integration
Changes MUST use short-lived branches, Pull Requests, review, and GitHub Actions. The base
branch is `master`. A change MUST pass the applicable build and automated checks before merge.

### X. Specification-First Improvements
Every improvement MUST start by updating or adding the applicable specification. Implementation,
tests, and verification MUST follow the approved specification rather than precede it.

## Technical Constraints

The current application is a C# Windows Forms application targeting .NET Framework 4.7.2.
Visual Studio 2026 is the supported development environment. The existing project structure,
designer support, and target framework MUST be preserved unless an approved specification
explicitly changes them.

## Development Workflow

Each change MUST follow this sequence: create or update the specification, resolve open
clarifications when requirements are ambiguous, produce and review a technical plan, derive and
review the implementation tasks, implement with GitHub Copilot using TDD, run the relevant
automated tests and build, verify the result against the specification, review the diff, and
submit a Pull Request against `master`. GitHub Actions MUST provide the repository checks used
to verify the change. The Pull Request description MUST identify the specification, plan, and
tasks that justify the change.

## Governance

This constitution supersedes conflicting development practices for GestorIncidencias. An
amendment MUST be proposed through an updated specification, reviewed in a Pull Request, and
recorded with a version change and rationale. Versioning follows semantic rules: MAJOR for
backward-incompatible governance changes, MINOR for new or materially expanded principles, and
PATCH for clarifications that do not change governance intent.

Every Pull Request MUST verify compliance with this constitution, including specification-first
planning, separation of business rules, designer compatibility, validation, and automated tests.
Exceptions MUST be documented in the Pull Request and approved by the reviewer before merge.

**Version**: 1.0.0 | **Ratified**: 2026-10-24 | **Last Amended**: 2026-10-24
